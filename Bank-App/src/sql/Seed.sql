--  Script: insert statement into table user
--triggers from schema sql update usder_cred aqnd accounts tables

INSERT INTO bankapp."user"
(user_id, f_name, l_name, user_address, phone_num, email)
VALUES
(?, ?, ?, ?, ?, ?);

INSERT INTO bankapp.transfers
(transfer_id, account_released_fk, account_received_fk, date_created,amount_transferred)
VALUES
(?, ?, ?, ?, ?);