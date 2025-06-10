# Modelo Lógico de Datos para ForgottenToDoApp (Completo)

---

## 1. Entidades y Atributos

### 1.1 Entidad `AreaDeEnfoque`
Representa una categoría principal o sub-categoría en el universo de tareas del usuario, con su posicionamiento visual en el mapa.

* **IdAreaDeEnfoque**: `UUID/GUID` (Identificador único, clave primaria)
* **Nombre**: `Cadena de Texto` (Nombre del área de enfoque, no nulo, único a nivel de su padre)
* **Descripcion**: `Cadena de Texto` (Descripción opcional del área)
* **IdAreaPadre**: `UUID/GUID` (Identificador del área de enfoque padre, nulo si es un área raíz, clave foránea a `AreaDeEnfoque.IdAreaDeEnfoque`)
* **ColorHex**: `Cadena de Texto` (Color asociado al área para visualización, ej. "#RRGGBB")
* **PosicionX**: `Número Decimal` (Coordenada X de la posición del área en el canvas del mapa. Valores aceptados: cualquier número real que represente una posición en la UI.)
* **PosicionY**: `Número Decimal` (Coordenada Y de la posición del área en el canvas del mapa. Valores aceptados: cualquier número real que represente una posición en la UI.)
* **OrdenVisual**: `Entero` (Define el orden de visualización de las áreas dentro de un mismo nivel. Esto es más para el orden lógico o de lista.)
* **FechaCreacion**: `Fecha y Hora` (Marca de tiempo de creación, no nulo)
* **FechaUltimaModificacion**: `Fecha y Hora` (Marca de tiempo de la última actualización, no nulo)

### 1.2 Entidad `Tarea`
Representa una actividad específica que el usuario debe realizar.

* **IdTarea**: `UUID/GUID` (Identificador único, clave primaria)
* **Titulo**: `Cadena de Texto` (Nombre conciso de la tarea, no nulo)
* **Descripcion**: `Cadena de Texto` (Detalles adicionales de la tarea, opcional)
* **FechaLimite**: `Fecha y Hora` (Fecha y hora límite para completar la tarea, opcional)
* **Prioridad**: `Entero` (Nivel de importancia, ej. 1=Alta, 2=Media, 3=Baja. Valores aceptados: `[1, 2, 3]`)
* **Estado**: `Cadena de Texto` (Estado actual de la tarea, ej. "Pendiente", "Completada", "En Progreso", "Archivada". Valores aceptados: `["Pendiente", "Completada", "En Progreso", "Archivada"]`)
* **TipoTarea**: `Cadena de Texto` (Clasificación de la tarea según su recurrencia. Valores aceptados: `["Unica", "Repetitiva No Acumulativa", "Repetitiva Acumulativa", "Habito"]`)
* **FechaCreacion**: `Fecha y Hora` (Marca de tiempo de creación, no nulo)
* **FechaUltimaModificacion**: `Fecha y Hora` (Marca de tiempo de la última actualización, no nulo)
* **IdAreaDeEnfoque**: `UUID/GUID` (Clave foránea a `AreaDeEnfoque.IdAreaDeEnfoque`. Una tarea pertenece a un área específica)
* **EsComun**: `Booleano` (Indica si la tarea es una "Tarea Común" que aplica a múltiples áreas. Valores aceptados: `True`, `False`)

### 1.3 Entidad `PropiedadRepeticion`
Contiene la lógica de repetición para tareas con `TipoTarea` "Repetitiva No Acumulativa", "Repetitiva Acumulativa" o "Habito".

* **IdPropiedadRepeticion**: `UUID/GUID` (Identificador único, clave primaria)
* **IdTarea**: `UUID/GUID` (Clave foránea a `Tarea.IdTarea`, no nulo, único)
* **Frecuencia**: `Cadena de Texto` (Ej. "Diaria", "Semanal", "Mensual", "Anual", "Especifica". Valores aceptados: `["Diaria", "Semanal", "Mensual", "Anual", "Especifica"]`)
* **Intervalo**: `Entero` (Cada cuántas unidades de `Frecuencia` se repite. Ej. cada 2 días, cada 3 semanas)
* **DiasDeSemana**: `Cadena de Texto` (Lista de días de la semana para repeticiones semanales, ej. "Lun,Mie,Vie", opcional)
* **DiaDelMes**: `Entero` (Día específico del mes para repeticiones mensuales, ej. 15, opcional)
* **MesDelAnio**: `Entero` (Mes específico del año para repeticiones anuales, ej. 1-12, opcional)
* **FechaInicioRepeticion**: `Fecha y Hora` (Desde cuándo comienza a repetirse la tarea, no nulo)
* **FechaFinRepeticion**: `Fecha y Hora` (Hasta cuándo se repite la tarea, opcional)
* **CompletadaUltimaFecha**: `Fecha y Hora` (La última fecha en que se marcó como completada la tarea repetitiva/hábito, útil para seguimiento)

### 1.4 Entidad `RelacionTareaComun`
Permite asociar una "Tarea Común" a múltiples `AreaDeEnfoque`. Solo aplica si `Tarea.EsComun` es `True`.

* **IdRelacionTareaComun**: `UUID/GUID` (Identificador único, clave primaria)
* **IdTareaComun**: `UUID/GUID` (Clave foránea a `Tarea.IdTarea`, donde `Tarea.EsComun` es `True`, no nulo)
* **IdAreaDeEnfoque**: `UUID/GUID` (Clave foránea a `AreaDeEnfoque.IdAreaDeEnfoque`, no nulo)

### 1.5 Entidad `GeometriaConexion`
Define los puntos de control (o "vértices") que forman una línea no recta entre un nodo padre y un nodo hijo en el mapa mental.

* **IdGeometriaConexion**: `UUID/GUID` (Identificador único, clave primaria)
* **IdAreaPadre**: `UUID/GUID` (Clave foránea a `AreaDeEnfoque.IdAreaDeEnfoque`, no nulo)
* **IdAreaHijo**: `UUID/GUID` (Clave foránea a `AreaDeEnfoque.IdAreaDeEnfoque`, no nulo, **combinación única con `IdAreaPadre`**)
* **OrdenVertice**: `Entero` (Orden secuencial de los puntos de control en la línea, comenzando desde el padre)
* **PosicionX**: `Número Decimal` (Coordenada X del punto de control)
* **PosicionY**: `Número Decimal` (Coordenada Y del punto de control)

---

## 2. Relaciones entre Entidades

Aquí se describen las relaciones clave:

* **`AreaDeEnfoque` (Padre) 1 : N `AreaDeEnfoque` (Hijo)**:
    * Una `AreaDeEnfoque` puede tener muchas `Sub-Áreas de Enfoque` (representado por `IdAreaPadre`).
    * Una `Sub-Área de Enfoque` tiene un solo `AreaDeEnfoque` padre.
    * Relación recursiva, permite la jerarquía del mapa mental.

* **`AreaDeEnfoque` 1 : N `Tarea`**
    * Una `AreaDeEnfoque` puede contener muchas `Tarea`s.
    * Cada `Tarea` pertenece a una única `AreaDeEnfoque` (excluyendo las Tareas Comunes a través de esta relación directa).

* **`Tarea` 1 : 1 `PropiedadRepeticion`**
    * Una `Tarea` de tipo repetitivo o hábito tiene exactamente una `PropiedadRepeticion` asociada.
    * Una `PropiedadRepeticion` pertenece a una única `Tarea`.

* **`Tarea` (donde EsComun=True) M : N `AreaDeEnfoque` (a través de `RelacionTareaComun`)**
    * Una `Tarea` marcada como común (`EsComun = True`) puede estar asociada a múltiples `AreaDeEnfoque`.
    * Una `AreaDeEnfoque` puede estar asociada a múltiples `Tarea`s Comunes.
    * La entidad `RelacionTareaComun` actúa como una tabla de unión para esta relación muchos-a-muchos.

* **`AreaDeEnfoque` (Padre) 1 : N `GeometriaConexion` (para IdAreaPadre)**
    * Un `AreaDeEnfoque` puede ser el origen de muchas conexiones gráficas.
* **`AreaDeEnfoque` (Hijo) 1 : N `GeometriaConexion` (para IdAreaHijo)**
    * Un `AreaDeEnfoque` puede ser el destino de muchas conexiones gráficas.
* **`GeometriaConexion` N : 1 `AreaDeEnfoque` (Padre) y N : 1 `AreaDeEnfoque` (Hijo)**
    * Cada `GeometriaConexion` se refiere a un `IdAreaPadre` y un `IdAreaHijo` específicos, representando una única conexión entre ellos.

---

## 3. Consideraciones sobre `GeometriaConexion`

* **Líneas Rectas por Defecto:** Si no existen registros en la entidad `GeometriaConexion` para un par específico `(IdAreaPadre, IdAreaHijo)`, la aplicación debe interpretar que la línea que los conecta en el mapa mental debe ser una línea recta que une el centro de ambos nodos.
* **Orden de los Vértices:** El atributo `OrdenVertice` es crucial para reconstruir la forma de una línea no recta. Los puntos de control deben ser procesados en orden secuencial, comenzando desde el origen (padre) y terminando en el destino (hijo).
* **Optimización de Almacenamiento:** Esta entidad solo se poblará cuando una conexión gráfica haya sido manipulada por el usuario (es decir, ya no es una línea recta predeterminada). Esto optimiza el almacenamiento al evitar guardar datos de geometría para todas las conexiones simples.

---

## 4. Consideraciones Futuras (para el Modelo Lógico)

* **Entidad `Usuario`:** Si en el futuro la aplicación se convierte en multiusuario o colaborativa, se requerirá una entidad `Usuario` que contenga información de perfil y se relacione con las `AreaDeEnfoque` y `Tarea` para asignar propiedad y permisos.
* **Entidad `RegistroCompletado` (para Hábitos y Tareas Repetitivas):** Para un seguimiento más robusto del progreso de hábitos o tareas repetitivas, especialmente en la vista "Seguimiento de Hábitos", se podría implementar una entidad separada para registrar cada instancia de compleción. Ej.:
    * **IdRegistroCompletado**: `UUID/GUID`
    * **IdTarea**: `UUID/GUID` (Clave foránea a `Tarea.IdTarea`)
    * **FechaCompletado**: `Fecha y Hora` (La fecha y hora exacta en que se completó la instancia)
* **Entidad `Recordatorio`:** Para funcionalidades de recordatorios detallados (más allá de la `FechaLimite` en `Tarea`), se podría añadir una entidad `Recordatorio` con atributos como `FechaHoraRecordatorio`, `TipoAlerta` (ej. notificación, email), `Mensaje`, y una relación con `Tarea`.