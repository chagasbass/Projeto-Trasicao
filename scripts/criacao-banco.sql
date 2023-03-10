-- SCRIPT DAS TABELAS

CREATE TABLE ENDERECOS(

ID  uniqueIdentifier NOT NULL PRIMARY KEY,
CEP VARCHAR(8) NOT NULL,
RUA VARCHAR(100) NOT NULL,
BAIRRO VARCHAR(50) NOT NULL,
CIDADE VARCHAR(50) NOT NULL,
ESTADO VARCHAR(50) NOT NULL,
DATA_CADASTRO DATE NOT NULL
);

CREATE TABLE CATEGORIAS(
ID  uniqueIdentifier NOT NULL PRIMARY KEY,
NOME VARCHAR(50) NOT NULL,
DESCRICAO VARCHAR(50) NOT NULL,
DATA_CADASTRO DATE NOT NULL
);

CREATE TABLE PRODUTOS(
ID  uniqueIdentifier NOT NULL PRIMARY KEY,
ID_CATEGORIA uniqueIdentifier NOT NULL,
NOME VARCHAR(50) NOT NULL,
DESCRICAO VARCHAR(50) NOT NULL,
PRECO DECIMAL(19,4) NOT NULL,
QUANTIDADE INT NOT NULL,
DATA_CADASTRO DATE NOT NULL,
FOREIGN KEY (ID_CATEGORIA) REFERENCES CATEGORIAS(ID)); 