CREATE EXTENSION IF NOT EXISTS "uuid-ossp";

CREATE TABLE IF NOT EXISTS person (
    id SERIAL PRIMARY KEY,
    name text NOT NULL,
    password text,
    email text NOT NULL UNIQUE,
    person_type text NOT NULL DEFAULT 'normal'
);

CREATE TABLE IF NOT EXISTS tenant(
    id SERIAL PRIMARY KEY,
    name text NOT NULL
);

CREATE TABLE IF NOT EXISTS person_in_tenant(
    person_id integer,
    tenant_id integer,
    CONSTRAINT person_fk FOREIGN KEY (person_id) REFERENCES person(id) ON DELETE CASCADE,
    CONSTRAINT tenant_fk FOREIGN KEY (tenant_id) REFERENCES tenant(id) ON DELETE CASCADE

);
CREATE TABLE IF NOT EXISTS report(
    id SERIAL PRIMARY KEY,
    name text NOT NULL,
    created_by integer,
    tenant_id integer,
    report_url text NOT NULL,
    --TODO: maby delete when the organization goes away??
    CONSTRAINT tenant_fk FOREIGN KEY (tenant_id) REFERENCES tenant(id) on DELETE SET NULL,
    CONSTRAINT created_by_person_fk FOREIGN KEY (created_by) REFERENCES person(id) on DELETE SET NULL
);

-- This is a Drug
CREATE TABLE IF NOT EXISTS product(
    id uuid PRIMARY KEY,
    country text NOT NULL DEFAULT 'EG',
    pharmacutical_form text NOT NULL,
    --TODO, can be enum
    route_of_adminstration text NOT NULL,
    product_lifetime text,
    authorization_number text,
    registration_date date NOT NULL,
    re_registration_date date,
    holder_name text
);
--
CREATE TABLE IF NOT EXISTS active_substance(
    id uuid PRIMARY KEY,
    name text NOT NULL,
    concentration decimal NOT NULL,
    conc_unit text NOT NULL
);

CREATE TABLE IF NOT EXISTS product_active_substance_map(
    id uuid PRIMARY KEY,
    product_id uuid,
    substance_id uuid,
    -- CONSTRAINTS
    CONSTRAINT product_id_fk FOREIGN KEY (product_id) REFERENCES product(id) on DELETE CASCADE,
    CONSTRAINT substance_id_fk FOREIGN KEY (substance_id) REFERENCES active_substance(id) on DELETE CASCADE
);

-- -- DRUG SECTION FOR REPORT
CREATE TABLE IF NOT EXISTS drug_report(
    id uuid PRIMARY KEY,
    -- Drug discontinied as a s result of incident
    drug_discontinued boolean NOT NULL,
    -- Concominnant, suspect, interacting
    drug_characterization text NOT NULL,
    batch_number text,
    dosage_amount integer,
    dosage_amount_unit text,
    num_sepatate_doses integer,
    num_units_per_interval integer,
    -- minute, hour, day
    interval_type text,
    cumulative_does_till_first_reaction integer,
    dosage_text text,
    gestation_period_at_exposure integer,
    -- days, weeks, months, trimester
    gestation_period_at_exposure_unit text,
    medDRA_version text,
    indication_for_use_in_case text,
    -- TIME INFORMATION
    drug_adminstration_start_date date NOT NULL,
    drug_adminstration_end_date date NOT NULL,
    time_interval_from_first_dose_till_reaction interval,
    time_interval_from_last_dose_till_reaction interval,
    duration_of_drug_adminstration23 interval,
    -- Drug Withdrawn, dose reduced, dose_increased, does does not change, unknown, N/A
    action_taken_with_drug text,
    reaction_reoccur_on_adminstration boolean,
    additional_info text,
    product_id uuid,
    -- CONSTRAINTS
    CONSTRAINT product_id_foreign_key FOREIGN KEY (product_id) REFERENCES product(id) ON DELETE SET NULL;
);

--DATA
INSERT INTO person(name, email, password ,person_type)
VALUES('Mohamed Hassanin', 'hassanin@udel.edu', 'password1', 'admin');
INSERT INTO tenant(name) VALUES('company1');
INSERT INTO person_in_tenant(person_id,tenant_id) VALUES(1,1);
INSERT INTO report(name,created_by,tenant_id,report_url) VALUES('drug1 report',1,1,'https://www.google.com');

