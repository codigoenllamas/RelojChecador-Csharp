/********************* ROLES **********************/

CREATE ROLE RDB$ADMIN;
/********************* UDFS ***********************/

/****************** GENERATORS ********************/

CREATE GENERATOR GD_ADD_CHECK;
CREATE GENERATOR GD_ADD_CONTADOR;
CREATE GENERATOR GD_ADD_REGISTRO;
/******************** DOMAINS *********************/

/******************* PROCEDURES ******************/

SET TERM ^ ;
CREATE PROCEDURE ADD_EMPLEADOS (
    EMP_NOM Varchar(120),
    EMP_SEX Char(1),
    EMP_FECNAC Timestamp,
    EMP_CURP Varchar(18),
    EMP_DIREC Varchar(300),
    EMP_TEL Varchar(24),
    EMP_CEL Varchar(24),
    EMP_DEPTO Varchar(200),
    EMP_PUESTO Varchar(200),
    EMP_COMEN Varchar(400),
    EMP_FOTO Blob sub_type 0,
    EMP_SZFOTO Integer )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE ADD_HORARIOS (
    IDEMPLEADO Integer,
    IDSECUENCIA Integer,
    TOTALCHK Integer,
    DIAS Varchar(3),
    ENT_A Time,
    SAL_A Time,
    ENT_B Time,
    SAL_B Time )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE CHECK_DIA (
    IDEMPLEADO Integer )
RETURNS (
    STATUS Varchar(8) )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE INSERT_CHECK (
    IDEMPLEADO Integer,
    MOVIMIENTO Varchar(8) )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE INSERT_MANUAL (
    IDEMPLEADO Integer,
    CHK_FECHA Timestamp,
    CHK_HORA Time,
    MOVIMIENTO Varchar(8),
    REF_DOCTO Varchar(200) )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE SPFECHA (
    VALOR Timestamp )
RETURNS (
    FECHA Varchar(10) )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

SET TERM ^ ;
CREATE PROCEDURE UPDATE_HUELLA (
    IDEMPLEADO Integer,
    EMP_DBHUELLA Blob sub_type 0 )
AS
BEGIN SUSPEND; END^
SET TERM ; ^

/******************** TABLES **********************/

CREATE TABLE DBMANEJO
(
  IDPOLITICA Integer NOT NULL,
  CHEQUEO_MINUTOS Integer DEFAULT 0,
  ASISTENCIA_MINUTOS Integer DEFAULT 0,
  RETARDO_MINUTOS Integer DEFAULT 0,
  FALTA_MINUTOS Integer DEFAULT 0,
  INICIAR_RELOJ Smallint NOT NULL,
  ONOMASTICO Smallint NOT NULL,
  CHEQUEO_ANTICIPADO Smallint NOT NULL,
  H_NO_DEFINIDO Smallint NOT NULL,
  PRIMARY KEY (IDPOLITICA)
);
CREATE TABLE HORASREAL
(
  IDMATRIZ Integer NOT NULL,
  IDEMPLEADO Integer NOT NULL,
  TOTALCHK Integer,
  CHK_FECHA Timestamp,
  DIAS Varchar(3),
  ENT_A Time,
  ST_ENT_A Varchar(2),
  SAL_A Time,
  ST_SAL_A Varchar(2),
  ENT_B Time,
  ST_ENT_B Varchar(2),
  SAL_B Time,
  ST_SAL_B Varchar(2),
  IDUSER Integer,
  REF_DOCTO Varchar(200)
);
CREATE TABLE MATRIZHORAS
(
  IDMATRIZ Integer NOT NULL,
  IDUSER Integer,
  FEC_ALT Date,
  IDEMPLEADO Integer NOT NULL,
  IDSECUENCIA Integer,
  TOTALCHK Integer,
  DIAS Varchar(3),
  ENT_A Time,
  SAL_A Time,
  ENT_B Time,
  SAL_B Time,
  CONSTRAINT PK_MATRIZHORAS_1 PRIMARY KEY (IDMATRIZ)
);
CREATE TABLE REGIS_DIARIO
(
  IDCONTROL Integer NOT NULL,
  IDEMPLEADO Integer DEFAULT 0,
  CHK_FECHA Timestamp,
  CHK_HORA Time,
  LAYER Integer,
  MOVIMIENTO Varchar(8),
  IDASISTENCIA Integer DEFAULT 0,
  CONTROL Varchar(150) NOT NULL,
  IDUSER Integer,
  REF_DOCTO Varchar(200),
  DIA Varchar(3),
  PRIMARY KEY (IDCONTROL)
);
CREATE TABLE SYSBACKUP
(
  ID Integer NOT NULL,
  FECHA Date,
  HORA Time,
  RESPALDO Smallint NOT NULL,
  PRIMARY KEY (ID)
);
CREATE TABLE TBEMPLEADO
(
  IDEMPLEADO Integer NOT NULL,
  EMP_NOM Varchar(120),
  EMP_SEX Char(1),
  EMP_FECNAC Timestamp,
  EMP_CURP Varchar(18),
  EMP_RFC Varchar(13),
  EMP_IMSS Varchar(15),
  EMP_DIREC Varchar(300),
  EMP_TEL Varchar(24),
  EMP_CEL Varchar(24),
  EMP_MAIL Varchar(120),
  EMP_DEPTO Varchar(200),
  EMP_PUESTO Varchar(200),
  EMP_COMEN Varchar(400),
  EMP_FECALT Timestamp,
  EMP_FOTO Blob sub_type 0,
  EMP_SZFOTO Integer,
  EMP_DBHUELLA Blob sub_type 0,
  EMP_SZHUELLA Integer,
  EMP_NIVELUS Varchar(8),
  EMP_NICK Varchar(14),
  EMP_PWD Varchar(5),
  EMP_TIPHOR Varchar(9),
  EMP_IDHORARIO Integer DEFAULT 0,
  EXTERNO Smallint,
  ACTIVO Char(2),
  PERMISOGPO Varchar(50),
  CONSTRAINT PK_TBEMPLEADO_1 PRIMARY KEY (IDEMPLEADO)
);
/********************* VIEWS **********************/

/******************* EXCEPTIONS *******************/

/******************** TRIGGERS ********************/

SET TERM ^ ;
CREATE TRIGGER AUTOINCRE_HORARIO FOR MATRIZHORAS ACTIVE
BEFORE INSERT POSITION 0
AS 
BEGIN 
    /* enter trigger code here */  
  IF(NEW.IDMATRIZ IS NULL) then 
  NEW.IDMATRIZ = gen_id(GD_ADD_CONTADOR,1); 
END^
SET TERM ; ^
SET TERM ^ ;
CREATE TRIGGER DISPARA_ALTA FOR TBEMPLEADO ACTIVE
BEFORE INSERT POSITION 0
AS 
BEGIN 
    /* enter trigger code here */  
  IF(NEW.IDEMPLEADO IS NULL) then 
  NEW.IDEMPLEADO = gen_id(GD_ADD_REGISTRO,1); 
END^
SET TERM ; ^
SET TERM ^ ;
CREATE TRIGGER DISPARA_CHEKAR FOR REGIS_DIARIO ACTIVE
BEFORE INSERT POSITION 0
AS 
BEGIN 
    /* enter trigger code here */  
  IF(NEW.IDCONTROL IS NULL) then 
  NEW.IDCONTROL = gen_id(GD_ADD_CHECK,1); 
END^
SET TERM ; ^

SET TERM ^ ;
ALTER PROCEDURE ADD_EMPLEADOS (
    EMP_NOM Varchar(120),
    EMP_SEX Char(1),
    EMP_FECNAC Timestamp,
    EMP_CURP Varchar(18),
    EMP_DIREC Varchar(300),
    EMP_TEL Varchar(24),
    EMP_CEL Varchar(24),
    EMP_DEPTO Varchar(200),
    EMP_PUESTO Varchar(200),
    EMP_COMEN Varchar(400),
    EMP_FOTO Blob sub_type 0,
    EMP_SZFOTO Integer )
AS
declare variable ID_EMPLE integer;

BEGIN       
ID_EMPLE=gen_id(GD_ADD_REGISTRO,1);    
  /* write your code here */                
INSERT INTO TBEMPLEADO(IDEMPLEADO, EMP_NOM, EMP_SEX, EMP_FECNAC, EMP_CURP, EMP_DIREC, EMP_TEL, EMP_CEL, EMP_DEPTO, EMP_PUESTO, EMP_COMEN, EMP_FECALT, EMP_FOTO, EMP_SZFOTO,EMP_NIVELUS,ACTIVO)
 VALUES(:ID_EMPLE, :EMP_NOM, :EMP_SEX, :EMP_FECNAC, :EMP_CURP, :EMP_DIREC, :EMP_TEL, :EMP_CEL, :EMP_DEPTO, :EMP_PUESTO, :EMP_COMEN, current_timestamp, :EMP_FOTO, :EMP_SZFOTO,'EMPLEADO','SI');

END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE ADD_HORARIOS (
    IDEMPLEADO Integer,
    IDSECUENCIA Integer,
    TOTALCHK Integer,
    DIAS Varchar(3),
    ENT_A Time,
    SAL_A Time,
    ENT_B Time,
    SAL_B Time )
AS
declare variable ID_MATRIZ integer;

begin 
ID_MATRIZ=gen_id(GD_ADD_CONTADOR,1);
INSERT INTO MATRIZHORAS( IDMATRIZ, IDEMPLEADO, IDSECUENCIA,TOTALCHK, DIAS, ENT_A, SAL_A, ENT_B, SAL_B)
    VALUES(:ID_MATRIZ, :IDEMPLEADO, :IDSECUENCIA, :TOTALCHK, :DIAS, :ENT_A, :SAL_A, :ENT_B, :SAL_B);
    
    END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE CHECK_DIA (
    IDEMPLEADO Integer )
RETURNS (
    STATUS Varchar(8) )
AS
DECLARE VARIABLE ENCONTRO VARCHAR(9); 
BEGIN
  /* write your code here */ 
 SELECT first(1) COALESCE(REGIS_DIARIO.MOVIMIENTO, '') FROM REGIS_DIARIO where REGIS_DIARIO.IDEMPLEADO
 =:IDEMPLEADO order by REGIS_DIARIO.CHK_FECHA desc
INTO :ENCONTRO;
IF (:ENCONTRO IS NULL ) THEN
STATUS = 'ENTRADA';
IF (:ENCONTRO ='ENTRADA' ) THEN
STATUS = 'SALIDA';
IF (:ENCONTRO ='SALIDA' ) THEN
STATUS = 'ENTRADA';



EXECUTE PROCEDURE INSERT_CHECK(:IDEMPLEADO,:STATUS);

SUSPEND;

END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE INSERT_CHECK (
    IDEMPLEADO Integer,
    MOVIMIENTO Varchar(8) )
AS
declare variable ID_CONTROL integer;
BEGIN       
ID_CONTROL=gen_id(GD_ADD_CHECK,1);         
  /* write your code here */           
INSERT INTO REGIS_DIARIO(IDCONTROL, IDEMPLEADO, MOVIMIENTO, CHK_FECHA, CHK_HORA,IDASISTENCIA, CONTROL,DIA )
VALUES(:ID_CONTROL, :IDEMPLEADO, :MOVIMIENTO, current_timestamp, current_time, 3,'DD', DECODE(
      EXTRACT(
         WEEKDAY FROM current_timestamp), 
            0, 'DOM', 
            1, 'LUN', 
            2, 'MAR', 
            3, 'MIE', 
            4, 'JUE', 
            5, 'VIE', 
            6, 'SAB') );

END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE INSERT_MANUAL (
    IDEMPLEADO Integer,
    CHK_FECHA Timestamp,
    CHK_HORA Time,
    MOVIMIENTO Varchar(8),
    REF_DOCTO Varchar(200) )
AS
declare variable ID_CONTROL integer;
BEGIN       
ID_CONTROL=gen_id(GD_ADD_CHECK,1);         
  /* write your code here */           
INSERT INTO REGIS_DIARIO(IDCONTROL, IDEMPLEADO, MOVIMIENTO, CHK_FECHA, CHK_HORA,IDASISTENCIA, CONTROL,REF_DOCTO,DIA )
VALUES(:ID_CONTROL, :IDEMPLEADO, :MOVIMIENTO, :CHK_FECHA, :CHK_HORA, 3,'MA',:REF_DOCTO,DECODE(
      EXTRACT(
         WEEKDAY FROM :CHK_FECHA), 
            0, 'DOM', 
            1, 'LUN', 
            2, 'MAR', 
            3, 'MIE', 
            4, 'JUE', 
            5, 'VIE', 
            6, 'SAB') );
END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE SPFECHA (
    VALOR Timestamp )
RETURNS (
    FECHA Varchar(10) )
AS
BEGIN
  FECHA = SUBSTRING(CAST(EXTRACT(DAY FROM VALOR) + 100 AS VARCHAR(3)) FROM 2 FOR 2);
  FECHA = FECHA || '/';
  FECHA = FECHA || SUBSTRING(CAST(EXTRACT(MONTH FROM VALOR) + 100 AS VARCHAR(3)) FROM 2 FOR 2);
  FECHA = FECHA || '/';
  FECHA = FECHA || CAST(EXTRACT(YEAR FROM VALOR) AS VARCHAR(4));
  suspend;
END^
SET TERM ; ^


SET TERM ^ ;
ALTER PROCEDURE UPDATE_HUELLA (
    IDEMPLEADO Integer,
    EMP_DBHUELLA Blob sub_type 0 )
AS
BEGIN   
  /* write your code here */   
UPDATE TBEMPLEADO SET TBEMPLEADO.EMP_DBHUELLA=:EMP_DBHUELLA where TBEMPLEADO.IDEMPLEADO=:IDEMPLEADO;  
  
END^
SET TERM ; ^


ALTER TABLE MATRIZHORAS ADD CONSTRAINT FK_MATRIZHORAS_1
  FOREIGN KEY (IDEMPLEADO) REFERENCES TBEMPLEADO (IDEMPLEADO);
ALTER TABLE MATRIZHORAS ADD CONSTRAINT FK_MATRIZHORAS_2
  FOREIGN KEY (IDUSER) REFERENCES TBEMPLEADO (IDEMPLEADO) ON UPDATE CASCADE;
ALTER TABLE REGIS_DIARIO ADD CONSTRAINT FK_REGIS_DIARIO_1
  FOREIGN KEY (IDEMPLEADO) REFERENCES TBEMPLEADO (IDEMPLEADO) ON UPDATE CASCADE ON DELETE CASCADE;
GRANT EXECUTE
 ON PROCEDURE ADD_EMPLEADOS TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE ADD_HORARIOS TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE CHECK_DIA TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE INSERT_CHECK TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE INSERT_MANUAL TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE SPFECHA TO  SYSDBA;

GRANT EXECUTE
 ON PROCEDURE UPDATE_HUELLA TO  SYSDBA;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON DBMANEJO TO  SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON HORASREAL TO  SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON MATRIZHORAS TO  SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON REGIS_DIARIO TO  SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON SYSBACKUP TO  SYSDBA WITH GRANT OPTION;

GRANT DELETE, INSERT, REFERENCES, SELECT, UPDATE
 ON TBEMPLEADO TO  SYSDBA WITH GRANT OPTION;

