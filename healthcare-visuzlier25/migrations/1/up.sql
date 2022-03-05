CREATE TYPE user_type AS ENUM ('admin', 'normal');
CREATE TABLE IF NOT EXISTS users (
    id SERIAL PRIMARY KEY,
    name text NOT NULL,
    email text NOT NULL UNIQUE,
    user_type user_type NOT NULL DEFAULT 'normal'
);
INSERT INTO users(name, email, user_type)
VALUES('Mohamed Hassanin', 'hassanin@udel.edu', 'admin');