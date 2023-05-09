-- FUCNITON: bankapp.create_account_func()

-- DROP FUNCTION bankapp.create_account_func()

CREATE OR REPLACE FUNCTION   bankapp.create_account_func()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLITILE NOT LEAKPROOF
AS $BODY$
declare
begin
    insert into bankapp.accounts(user_fk, balance , l_name) values (new.user, 1000.0, new.l_name);
return new;
end;
$BODY$;

ALTER FUNTCION bankapp.create_account_func()
    OWNER TO postgres;

-- FUNCTION: bankapp.received_trasnsfer_func()

-- DROP FUNCTION bankapp.received_transfer_func()

CREATE OR REPLACE FUNCTION bankapp.received_transfer_func()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLITILE NOT LEAKPROOF
AS $BODY$
declare
begin
update bankapp.accounts
set balance = balance + new.amount_transferred
where account_id = (select account_release_fk from bankapp.transfers where transfer_id = new.transfer_id);
return new;
end;
$BODY$;

ALTER FUNCTION bankapp.received_transfer_func
    OWNER TO postgres

-- FUNCTION: bankapp.received_trasnsfer_func()

-- DROP FUNCTION bankapp.received_transfer_func()

CREATE OR REPLACE FUNCTION bankapp.released_transfer_func()
    RETURNS trigger
    LANGUAGE 'plpgsql'
    COST 100
    VOLITILE NOT LEAKPROOF
AS $BODY$
declare
begin
update bankapp.accounts
set balance = balance - new.amount_transferred
where account_id = (select account_release_fk from bankapp.transfers where transfer_id = new.transfer_id);
return new;
end;
$BODY$;

ALTER FUNCTION bankapp.released_transfer_func
    OWNER TO postgres