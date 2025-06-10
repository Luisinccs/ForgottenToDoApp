# Modelo Lógico de Procesos para ForgottenToDoApp

---

Este modelo describe las principales funciones de la aplicación, cómo interactúan con los datos y qué eventos los activan.

### 1. Proceso de Gestión de Áreas de Enfoque

**Descripción:** Permite al usuario organizar su universo de tareas en una estructura jerárquica de Áreas de Enfoque y Sub-Áreas.

* **Entradas:** Nombre, Descripción, Color, Posición (X, Y), Área Padre (opcional).
* **Salidas:** Área de Enfoque creada/modificada/eliminada.
* **Eventos/Triggers:**
    * **Crear Área de Enfoque:** El usuario inicia la creación de un nuevo nodo en el mapa o en una lista.
    * **Editar Área de Enfoque:** El usuario modifica los detalles de un nodo existente.
    * **Mover Área de Enfoque:** El usuario arrastra un nodo a una nueva posición en el mapa.
    * **Eliminar Área de Enfoque:** El usuario decide eliminar un nodo (implica manejo de hijos y tareas asociadas).
    * **Modificar Conexión Gráfica:** El usuario edita la línea que conecta un área padre con un área hija (crea o actualiza `GeometriaConexion`).
* **Procesos Internos:**
    * Validación de datos de entrada (nombre, color).
    * Asignación de `UUID` único.
    * Actualización/inserción/eliminación en la entidad `AreaDeEnfoque`.
    * Para mover: Actualización de `PosicionX` y `PosicionY`.
    * Para eliminar: Puede requerir la eliminación en cascada o reasignación de hijos y tareas.
    * Para modificar conexión: Creación/actualización de registros en `GeometriaConexion`.

### 2. Proceso de Gestión de Tareas

**Descripción:** Permite al usuario crear y administrar sus tareas individuales, asignándolas a Áreas de Enfoque y definiendo sus propiedades.

* **Entradas:** Título, Descripción, Fecha Límite, Prioridad, Estado, Tipo de Tarea, Área de Enfoque asociada, indicador "Es Común".
* **Salidas:** Tarea creada/modificada/eliminada.
* **Eventos/Triggers:**
    * **Crear Tarea:** El usuario añade una nueva tarea a un Área de Enfoque específica.
    * **Editar Tarea:** El usuario actualiza los detalles de una tarea existente.
    * **Marcar Tarea como Completada/Incompleta:** El usuario cambia el estado de una tarea.
    * **Eliminar Tarea:** El usuario borra una tarea.
* **Procesos Internos:**
    * Validación de datos de entrada (título, área asociada).
    * Asignación de `UUID` único.
    * Actualización/inserción/eliminación en la entidad `Tarea`.
    * Si es una "Tarea Común" (`EsComun = True`): Gestión de `RelacionTareaComun`.

### 3. Proceso de Gestión de Propiedades de Repetición / Hábitos

**Descripción:** Administra la lógica de repetición para tareas recurrentes y el seguimiento de hábitos.

* **Entradas:** Frecuencia, Intervalo, Días de Semana, Día del Mes, Mes del Año, Fechas de Inicio/Fin de Repetición.
* **Salidas:** Propiedad de Repetición creada/modificada.
* **Eventos/Triggers:**
    * **Configurar Repetición:** El usuario define una tarea como repetitiva o un hábito.
    * **Actualizar Repetición:** El usuario modifica la frecuencia o el rango de una tarea repetitiva/hábito.
    * **Marcar Tarea Repetitiva/Hábito como Completada (Instancia):** El usuario completa una instancia de la tarea para una fecha específica (actualiza `CompletadaUltimaFecha` en `PropiedadRepeticion`, o crea una entrada en `RegistroCompletado` si se implementa).
* **Procesos Internos:**
    * Validación de la lógica de repetición.
    * Asignación de `UUID` único a `PropiedadRepeticion`.
    * Actualización/inserción/eliminación en la entidad `PropiedadRepeticion`.
    * Lógica para determinar si una instancia de una tarea repetitiva está "vencida" o "vigente".

### 4. Proceso de Navegación y Visualización de Vistas

**Descripción:** Gestiona la presentación de la información al usuario a través de las diferentes vistas de la aplicación.

* **Entradas:** Selección de área de enfoque, selección de rango de calendario, selección de intervalo de hábitos.
* **Salidas:** Vista del Mapa, Vista de Lista de Listas, Vista de Calendario, Vista de Seguimiento de Hábitos.
* **Eventos/Triggers:**
    * **Cargar Vista de Mapa:** Al iniciar la aplicación o al seleccionar la opción de mapa.
    * **Seleccionar Área de Enfoque:** El usuario toca un nodo en el mapa.
    * **Navegar a Vista de Lista:** El usuario selecciona ver las tareas de un área.
    * **Navegar a Vista de Calendario:** El usuario selecciona ver el calendario.
    * **Cambiar Rango de Calendario:** El usuario ajusta la vista del calendario (día, semana, mes, año).
    * **Navegar a Vista de Hábitos:** El usuario selecciona la vista de seguimiento de hábitos.
    * **Cambiar Intervalo de Hábitos:** El usuario ajusta el rango de tiempo para el seguimiento de hábitos.
* **Procesos Internos:**
    * Recuperación de datos desde `AreaDeEnfoque`, `Tarea`, `PropiedadRepeticion` y `GeometriaConexion`.
    * Filtrado y ordenamiento de tareas por fecha, prioridad, área.
    * Construcción de los modelos de vista (ViewModels) a partir de los datos.
    * Lógica de renderizado del mapa mental (usando `PosicionX`, `PosicionY` y `GeometriaConexion`).
    * Lógica de calendario: Calcular qué tareas caen en el rango de fechas seleccionado.
    * Lógica de hábitos: Filtrar tareas de tipo "Habito" para el rango semanal.

### 5. Proceso de Sincronización y Persistencia (Implícito)

**Descripción:** Asegura que los cambios en los datos se guarden de forma segura y se recuperen al iniciar la aplicación.

* **Entradas:** Cambios en cualquier entidad de datos.
* **Salidas:** Datos persistidos y disponibles.
* **Eventos/Triggers:**
    * **Guardar Cambios:** Automáticamente al realizar una operación de creación/edición/eliminación.
    * **Cargar Datos Iniciales:** Al iniciar la aplicación.
* **Procesos Internos:**
    * Serialización/Deserialización de datos.
    * Interacción con la capa de acceso a datos (no definida a nivel lógico).
    * Manejo de errores de persistencia.