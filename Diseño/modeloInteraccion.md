# Modelo Conceptual de Interacción con el Usuario para ForgottenToDoApp

---

Este modelo describe la forma en que el usuario percibirá y operará la aplicación ForgottenToDoApp, enfocándose en su **modelo mental** del sistema y las **interacciones clave**.

### 1. Conceptos Centrales del Usuario

* **Mi Universo de Tareas:** El usuario percibirá esto como su **centro de control personal**, un mapa visual que representa todas sus responsabilidades (profesionales y personales) de alto nivel, como si fuera un cerebro que conecta todas sus ideas.
* **Áreas de Enfoque (y Sub-Áreas):** Son las **categorías principales y sub-categorías** dentro de su universo. El usuario las verá como "nodos" o "burbujas" en el mapa, que representan proyectos, áreas de estudio, aspectos de la vida personal, etc. Entenderá que estas áreas pueden contener otras más pequeñas para una mayor organización.
* **Tareas:** Son las **acciones concretas** que debe realizar. El usuario las asociará directamente a un Área de Enfoque específica. Distinguirá claramente entre tareas que se hacen una vez, las que se repiten sin acumularse (hábitos o ejercicios) y las que sí acumulan si no se cumplen (pagos).
* **Conexiones Visuales:** Las líneas entre las Áreas de Enfoque en el mapa no son solo visuales, el usuario entenderá que representan la **relación jerárquica** (padre-hijo) y que son manipulables para organizar mejor su "espacio mental".
* **Tareas Comunes:** El usuario las percibirá como una única tarea que "toca" o "afecta" a múltiples áreas de su universo, visible en el mapa a través de conexiones especiales.
* **Lista de Listas:** Una vista detallada que muestra las tareas dentro de un Área de Enfoque seleccionada, permitiéndole sumergirse en los detalles.
* **Calendario:** Un organizador temporal donde puede ver y gestionar tareas por día, semana, mes o año.
* **Seguimiento de Hábitos:** Una herramienta para monitorear su progreso en actividades recurrentes que busca mantener o mejorar.

### 2. Interacciones Clave del Usuario

* **Navegación en el Mapa de Enfoque:**
    * El usuario comenzará siempre en el "Mi Universo de Tareas".
    * Hará *taps* o *clics* en las "Áreas de Enfoque" para **profundizar** y ver sus sub-áreas o tareas asociadas en una vista de "Lista de Listas".
    * Arrastrará los nodos de "Áreas de Enfoque" para **reorganizar** su ubicación visual en el mapa, reflejando su importancia o relación espacial percibida.
    * Manipulará las líneas de conexión para **ajustar la trayectoria** entre nodos, creando un mapa personalizado y estético.
* **Gestión de Áreas de Enfoque:**
    * Creará nuevas "Áreas de Enfoque" directamente desde el mapa, añadiendo un nodo padre o un nodo hijo.
    * Editará los nombres, descripciones y colores de las áreas para personalizarlas.
    * Eliminará áreas, entendiendo que esto podría afectar a las sub-áreas y tareas contenidas.
* **Gestión de Tareas:**
    * Añadirá tareas a un Área de Enfoque específica, una vez que haya navegado a la vista de "Lista de Listas".
    * Marcará tareas como completadas con un simple *tap* o clic en una casilla de verificación.
    * Definirá las propiedades de la tarea, como título, fecha límite, prioridad y tipo (única, repetitiva no acumulativa, repetitiva acumulativa, hábito).
    * Asignará tareas como "comunes" y las vinculará a múltiples Áreas de Enfoque.
* **Uso del Calendario:**
    * Cambiará la vista del calendario entre día, semana, mes y año para una planificación flexible.
    * Verá las tareas programadas y sus fechas límite en el calendario.
    * Podrá interactuar con las tareas directamente desde la vista del calendario (ej. marcar como completada).
* **Uso del Seguimiento de Hábitos:**
    * Accederá a una vista dedicada que muestra sus hábitos semanales.
    * Marcará los hábitos como completados para cada día, visualizando su progreso.
* **Persistencia de Datos:** El usuario esperará que todos sus cambios (creación de áreas, tareas, movimientos en el mapa, estados de tareas) se **guarden automáticamente** y estén disponibles al reabrir la aplicación, sin necesidad de guardar manualmente.

### 3. Flujo de Usuario de Alto Nivel

1.  **Inicio de la Aplicación:** El usuario ve su "Mi Universo de Tareas" (el mapa mental completo) como su punto de partida.
2.  **Revisión General:** Inspecciona el mapa para obtener una visión general de sus áreas de responsabilidad.
3.  **Enfoque en un Área:** Selecciona una "Área de Enfoque" (o "Sub-Área") específica en el mapa para sumergirse en ella.
4.  **Gestión Detallada:** En la vista de "Lista de Listas" o "Calendario" o "Seguimiento de Hábitos" asociada, gestiona sus tareas y hábitos específicos de esa área o período.
5.  **Retorno:** Vuelve al "Mi Universo de Tareas" para reevaluar su panorama general y elegir el próximo enfoque.
6.  **Edición del Mapa:** En cualquier momento, puede volver al mapa para añadir nuevas áreas, reorganizar las existentes o ajustar las conexiones, adaptando el mapa a su evolución mental.

---