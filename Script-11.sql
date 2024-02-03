CREATE TABLE users (
    id SERIAL PRIMARY KEY,
    user_name VARCHAR(255) NOT NULL,
    email VARCHAR(255) NOT NULL,
    password_hash VARCHAR(255) NOT NULL,
    roles VARCHAR(255)[] DEFAULT '{}',
    -- Thêm các trường thông tin khác của người dùng (nếu cần)
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

CREATE OR REPLACE FUNCTION public.register_user(p_username character varying , p_email character varying , p_password character varying)
 RETURNS refcursor
 LANGUAGE plpgsql
AS $function$ 
/*
  
  Created by: 227358-Xuân Tiến
  Date: 2023/12/05
 */
DECLARE
 ref refcursor ='v_out';
 v_return int4 :=0;
BEGIN
 
 INSERT INTO public.users
 (id, user_name, email, password_hash)
 VALUES(nextval('users_id_seq'::regclass), p_username, p_email, p_password) returning id into v_return ;
 OPEN ref FOR
  SELECT v_return;
 RETURN ref;
END;
$function$
;
SELECT id, user_name, email, password_hash, roles, created_at, updated_at
FROM public.users;

CREATE OR REPLACE FUNCTION public.get_all_users()
 RETURNS refcursor
 LANGUAGE plpgsql
AS $function$
    DECLARE
      v_out refcursor:='v_out';                                                     -- Declare a cursor variable
    BEGIN
      OPEN v_out FOR 
      SELECT id, user_name, email, password_hash, roles, created_at, updated_at
FROM public.users;
    RETURN v_out;                                                       -- Return the cursor to the caller
    END;
    $function$
;
CREATE OR REPLACE FUNCTION public.get_user_by_email(p_email character varying)
 RETURNS refcursor
 LANGUAGE plpgsql
AS $function$
    DECLARE
      v_out refcursor:='v_out';                                                     -- Declare a cursor variable
    BEGIN
      OPEN v_out FOR 
      SELECT id, user_name, email, password_hash, roles, created_at, updated_at
FROM public.users where email=p_email;
    RETURN v_out;                                                       -- Return the cursor to the caller
    END;
    $function$
;
COMMIT;
BEGIN;
 SELECT get_user_by_email('string');
 FETCH ALL IN v_out;
COMMIT;