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
    id uuid DEFAULT uuid_generate_v4 () PRIMARY KEY,
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
    id uuid DEFAULT uuid_generate_v4 () PRIMARY KEY,
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
CREATE TABLE IF NOT EXISTS person_reporter
(
     id uuid DEFAULT uuid_generate_v4 () PRIMARY KEY,
     first_name text NOT NULL,
     last_name text NOT NULL,
     title text NOT NULL,
     phone_number text NOT NULL,
     person_type text NOT NULL DEFAULT 'DOCTOR'
);

CREATE TABLE IF NOT EXISTS data_user
(
    id uuid DEFAULT uuid_generate_v4 () PRIMARY KEY,
    title text NOT NULL,
    department text NOT NULL,
    first_name text NOT NULL,
    middle_name text NOT NULL,
    last_name text NOT NULL,
    telephone text NOT NULL,
    email_address text NOT NULL,
    tenant_id integer NOT NULL,
    --CONSTRAINTS
     CONSTRAINT tenant_id_fk FOREIGN KEY (tenant_id) REFERENCES tenant(id) on DELETE CASCADE
);
--DATA
INSERT INTO person(name, email, password ,person_type)
VALUES('Mohamed Hassanin', 'hassanin@udel.edu', 'password1', 'admin');
INSERT INTO tenant(name) VALUES('company1');
INSERT INTO person_in_tenant(person_id,tenant_id) VALUES(1,1);
-- INSERT INTO report(name,created_by,tenant_id,report_url) VALUES('drug1 report',1,1,'https://www.google.com');

