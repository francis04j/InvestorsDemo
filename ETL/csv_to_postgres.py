import csv
import os
import logging
import psycopg2
from psycopg2 import sql
from datetime import datetime
from tqdm import tqdm
from dotenv import load_dotenv

# Load environment variables
load_dotenv()

# Configure logging
logging.basicConfig(
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s',
    handlers=[
        logging.FileHandler('data_import.log'),
        logging.StreamHandler()
    ]
)

# Database configuration from environment
DB_CONFIG = {
    "host": os.getenv('DB_HOST', 'localhost'),
    "database": os.getenv('DB_NAME', 'investors'),
    "user": os.getenv('DB_USER', 'postgres'),
    "password": os.getenv('DB_PASSWORD', 'postgres'),
    "port": os.getenv('DB_PORT', 5432)
}

CSV_FILE = "data.csv"


def validate_row(row, row_num):
    """Validate CSV row and return error messages"""
    errors = []

    
    # Date validation
    date_fields = ['Investor Date Added', 'Investor Last Updated']
    for field in date_fields:
        try:
            datetime.strptime(row[field], '%Y-%m-%d').date()
        except ValueError:
            errors.append(f"Invalid date format in {field}: {row[field]}")
    
    return errors

def create_tables(conn):
    """Create database tables if they don't exist"""
    with conn.cursor() as cur:
        cur.execute("""
            CREATE TABLE IF NOT EXISTS investors (
                investor_id SERIAL PRIMARY KEY,
                name VARCHAR(255) UNIQUE NOT NULL,
                type VARCHAR(100),
                country VARCHAR(100),
                date_added DATE,
                last_updated DATE
            )
        """)
        
        cur.execute("""
            CREATE TABLE IF NOT EXISTS commitments (
                commitment_id SERIAL PRIMARY KEY,
                investor_id INTEGER REFERENCES investors(investor_id),
                asset_class VARCHAR(100) NOT NULL,
                amount NUMERIC(18,2) NOT NULL,
                currency CHAR(3) NOT NULL
            )
        """)
        conn.commit()

def get_or_create_investor(conn, investor_data):
    """Get existing investor or create new one"""
    with conn.cursor() as cur:
        try:
            cur.execute(
                sql.SQL("SELECT investor_id FROM investors WHERE name = %s"),
                [investor_data['name']]
            )
            result = cur.fetchone()
            
            if result:
                return result[0]
                
            cur.execute(
                sql.SQL("""
                    INSERT INTO investors 
                    (name, type, country, date_added, last_updated)
                    VALUES (%(name)s, %(type)s, %(country)s, 
                            %(date_added)s, %(last_updated)s)
                    RETURNING investor_id
                """),
                investor_data
            )
            return cur.fetchone()[0]
        except Exception as e:
            logging.error(f"Investor error: {str(e)}")
            raise

def process_csv(file_path, conn):
    """Process CSV file and insert validated data into database"""
    try:
        with open(file_path, 'r') as f:
            reader = csv.DictReader(f)
            total_rows = sum(1 for _ in reader)  # Get total row count
            f.seek(0)  # Reset file pointer
            reader = csv.DictReader(f)  # Recreate iterator
            
            success_count = 0
            error_count = 0
            
            with tqdm(total=total_rows, desc="Processing CSV") as pbar:
                for row_num, row in enumerate(reader, 1):
                    try:
                        # Validate row
                        validation_errors = validate_row(row, row_num)
                        if validation_errors:
                            error_msg = f"Row {row_num} errors: " + "; ".join(validation_errors)
                            logging.error(error_msg)
                            error_count += 1
                            continue

                        # Prepare investor data
                        investor_data = {
                            'name': row['Investor Name'],
                            'type': row['Investory Type'],
                            'country': row['Investor Country'],
                            'date_added': datetime.strptime(row['Investor Date Added'], '%Y-%m-%d').date(),
                            'last_updated': datetime.strptime(row['Investor Last Updated'], '%Y-%m-%d').date()
                        }
                        
                        # Get/Create investor and get ID
                        investor_id = get_or_create_investor(conn, investor_data)
                        
                        # Insert commitment
                        with conn.cursor() as cur:
                            cur.execute(
                                sql.SQL("""
                                    INSERT INTO commitments 
                                    (investor_id, asset_class, amount, currency)
                                    VALUES (%s, %s, %s, %s)
                                """),
                                (
                                    investor_id,
                                    row['Commitment Asset Class'],
                                    float(row['Commitment Amount']),
                                    row['Commitment Currency']
                                )
                            )
                        conn.commit()
                        success_count += 1
                        
                    except Exception as e:
                        logging.error(f"Row {row_num} failed: {str(e)}")
                        conn.rollback()
                        error_count += 1
                    
                    pbar.update(1)
            
            logging.info(f"\nImport complete: {success_count} successful, {error_count} errors")

    except FileNotFoundError:
        logging.error(f"CSV file not found: {file_path}")
    except Exception as e:
        logging.error(f"Fatal error: {str(e)}")

if __name__ == "__main__":
    try:
        conn = psycopg2.connect(**DB_CONFIG)
        create_tables(conn)
        process_csv(CSV_FILE, conn)
    except psycopg2.OperationalError as e:
        logging.error(f"Database connection failed: {str(e)}")
    except Exception as e:
        logging.error(f"Unexpected error: {str(e)}")
    finally:
        if 'conn' in locals():
            conn.close()