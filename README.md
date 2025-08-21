# GESTOR DE TORNEOS
Juliana Pallares

# Base De Datos
```sql
CREATE DaTABASE gestortorneos

USE gestortorneos

CREATE TABLE IF NOT EXISTS torneos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50) NOT NULL,
    FechaInicio DATE NOT NULL,
    FechaFin DATE NOT NULL
) ENGINE = INNODB;

CREATE TABLE IF NOT EXISTS equipos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Tipo VARCHAR(50),
    Pais VARCHAR(50) 
) ENGINE = INNODB;

CREATE TABLE IF NOT EXISTS torneo_equipo (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    TorneoId INT NOT NULL,
    EquipoId INT NOT NULL,
    FOREIGN KEY (TorneoId) REFERENCES torneos(Id),
    FOREIGN KEY (EquipoId) REFERENCES equipos(Id)
) ENGINE = INNODB;

CREATE TABLE IF NOT EXISTS cuerposmedicos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Especialidad VARCHAR(100) NOT NULL,
    EquipoId INT NOT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipos(Id)
) ENGINE = INNODB;

CREATE TABLE IF NOT EXISTS cuerpostecnicos (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Cargo VARCHAR(100) NOT NULL,
    EquipoId INT NOT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipos(Id)
) ENGINE = INNODB;

CREATE TABLE IF NOT EXISTS jugadores (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Apellido VARCHAR(100) NOT NULL,
    Edad INT NOT NULL,
    Dorsal INT NOT NULL,
    Posicion VARCHAR(50),
    EquipoId INT NULL,
    FOREIGN KEY (EquipoId) REFERENCES Equipos(Id)
) ENGINE = INNODB;

CREATE TABLE Notificaciones (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    Mensaje NVARCHAR(500) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Estado NVARCHAR(50) NOT NULL,
    EquipoDuenoId INT NOT NULL,
    EquipoSolicitanteId INT NOT NULL,
    JugadorId INT NOT NULL,
    Atendida BIT NOT NULL DEFAULT 0,
    FOREIGN KEY (EquipoDuenoId) REFERENCES Equipos(Id),
    FOREIGN KEY (EquipoSolicitanteId) REFERENCES Equipos(Id),
    FOREIGN KEY (JugadorId) REFERENCES Jugadores(Id)
);
CREATE TABLE Transferencias (
    Id INT AUTO_INCREMENT PRIMARY KEY,
    JugadorId INT NOT NULL,
    EquipoOrigenId INT NOT NULL,
    EquipoDestinoId INT NOT NULL,
    Tipo NVARCHAR(50) NOT NULL,  
    Precio DECIMAL(18,2) NULL,   
    TiempoPrestamoMeses INT NULL, 
    Estado NVARCHAR(50) NOT NULL DEFAULT 'Pendiente',
    FechaSolicitud DATETIME NOT NULL DEFAULT GETDATE(),
    FechaRespuesta DATETIME NULL,
    FOREIGN KEY (JugadorId) REFERENCES Jugadores(Id),
    FOREIGN KEY (EquipoOrigenId) REFERENCES Equipos(Id),
    FOREIGN KEY (EquipoDestinoId) REFERENCES Equipos(Id)
);


```