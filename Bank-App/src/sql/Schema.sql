create schema bankapp

-- Table: bankapp.accounts

-- DROP TABLE bankapp.accounts

CREATE TABLE IF NOT EXISTS bankapp.accounts
(
    account_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1),
    user_fk integer NOT NULL,
    balance money NOT NULL,
    routing_num integer NOT NULL DEFAULT ((random() * (9)::double precision) + (1)::double precision),
    date_created timestamp without time zone NOT NULL DEFAULT now(),
    l_name character varying COLLATE pg_catalog."default",
    CONSTRAINT accounts_pkey PRIMARY KEY (account_id),
    CONSTRAINT accounts_user_fk_fkey FOREIGN KEY (user_fk),
        REFERENCES bankapp."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)''

TABLESPACE pg_default;

ALTER TABLE bankapp.accounts
    OWNER TO postgres;

-- Table: bankapp.transfers

-- DROP TABLE bankapp.transfers

CREATE TABLE IF NOT EXISTS bankapp.transfers
(
    transfer_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1),
    account_released_fk integer NOT NULL,
    account_received_fk integer NOT NULL,
    date_created timestamp without time zone NOT NULL DEFAULT now(),
    amount_transferred money NOT NULL,
    CONSTRAINT transfers_pkey PRIMARY KEY (transfer_id),
    CONSTRAINT transfers_account_released_fk_fkey FOREIGN KEY (account_released_fk)
        REFERENCES bankapp.accounts (account_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT transfers_account_received_fk_fkey FOREIGN KEY (account_received_fk)
        REFERENCES bankapp.accounts (account_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE bankapp.transfers
    OWNER TO postgres;

-- Trigger: update_account_received_trg

-- DROP TRIGGER update_account_received_trg ON bankapp.transfers;

CREATE TRIGGER update_account_received_trg
    AFTER INSERT OR UPDATE
    ON bankapp.transfers
    FOR EACH ROW
    EXECUTE FUNCTION bankapp.received_transfer_func();

-- Trigger: update_account_released_trg

-- DROP TRIGGER update_account_released_trg ON bankapp.transfers;

CREATE TRIGGER update_account_released_trg
    AFTER INSERT OR UPDATE
    ON bankapp.transfers
    FOR EACH ROW
    EXECUTE FUNCTION bankapp.released_transfer_func();

-- Table: bankapp.user

-- DROP TABLE bankapp."user";

CREATE TABLE IF NOT EXISTS bankapp."user"
(
    user_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1),
    f_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    l_name character varying(50) COLLATE pg_catalog."default" NOT NULL,
    user_address character varying COLLATE pg_catalog."default" NOT NULL,
    phone_num character(10) COLLATE pg_catalog."default" NOT NULL,
    email character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT user_pkey PRIMARY KEY (user_id)
)

TABLESPACE pg_default;

ALTER TABLE  bankapp."user"
    OWNER TO postgres;

-- Trigger: create_account_trg

-- DROP TIGGER create_account_trg ON bankapp."user";

CREATE TRIGGER create_account_trg
    AFTER INSERT OR UPDATE
    ON bankapp."user"
    FOR EACH ROW
    EXECUTE FUNCTION bankapp.create_account_func();

-- Table: bankapp.user_cred

-- DROP TABLE bankapp.user_cred;

CREATE TABLE IF NOT EXISTS bankapp.user_cred
(
    user_cred_id integer NOT NULL GENERATED ALWAYS AS IDENTITY ( INCREMENT 1 START 1 MINVALUE 1 MAXVALUE 2147483647 CACHE 1),
    user_fk integer NOT NULL,
    user_name character varying(40) COLLATE pg_catalog."default",
    salt uuid NOT NULL,
    pass_hash character varying COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT user_cred_pkey PRIMARY KEY (user_cred_id),
    CONSTRAINT user_cred_user_fk_fkey FOREIGN KEY (user_fk)
        REFERENCES bankapp."user" (user_id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE bankapp.user_cred
    OWNER to postgres;