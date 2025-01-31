## Set up
> python3 -m venv venv
> source venv/bin/activate

### Copy example env file
> cp .env.example .env

### Install dependencies
> pip install -r requirements.txt

if error, do manual install
> pip install psycopg2-binary
> pip install python-dotenv
> pip install tqdm


### (optional) Start Postgres if not already started
docker-compose up -d

# Run importer 
> python csv_to_postgres.py

