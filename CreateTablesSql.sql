-- PostgreSQL script
DO
$do$
BEGIN
   IF NOT EXISTS (
      SELECT FROM pg_database
      WHERE datname = 'Investors'
   ) THEN
      PERFORM dblink_exec('dbname=postgres', 'CREATE DATABASE "Investors"');
   END IF;
END
$do$;

-- Connect to the Investors database
\c Investors;

CREATE TABLE IF NOT EXISTS Investors (
    InvestorId SERIAL PRIMARY KEY,
    Name VARCHAR(100) NOT NULL,
    Type VARCHAR(50),
    Country VARCHAR(50),
    DateAdded DATE,
    LastUpdated DATE
);

CREATE TABLE IF NOT EXISTS Commitments (
    CommitmentId SERIAL PRIMARY KEY,
    InvestorId INT REFERENCES Investors(InvestorId),
    AssetClass VARCHAR(50) NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    Currency VARCHAR(3) NOT NULL
);