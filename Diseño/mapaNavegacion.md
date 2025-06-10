# Mapa de Navegación de ForgottenToDoApp

---

## Puntos de Entrada y Flujos Principales

### 1. Vista Principal: Mi Universo de Tareas (Mapa de Enfoque)
    * **Punto de Inicio de la Aplicación.**
    * **Permite al usuario:**
        * Visualizar y manipular gráficamente las Áreas de Enfoque.
        * Acceder a otras vistas principales.

### 2. Desde "Mi Universo de Tareas" (Mapa de Enfoque) se puede navegar a:

    * **2.1. Vista "Lista de Listas"**
        * **Acceso:** Seleccionando un nodo de "Área de Enfoque" o "Sub-Área de Enfoque" en el mapa.
        * **Contenido:** Muestra las tareas asociadas al área seleccionada y sus sub-áreas anidadas.
        * **Navegación desde aquí:**
            * Volver a "Mi Universo de Tareas" (botón/gesto de retroceso).
            * Navegar a "Crear Tarea" (botón de añadir).
            * Navegar a "Editar Tarea" (seleccionando una tarea existente).

    * **2.2. Vista "Calendario"**
        * **Acceso:** Desde un botón o elemento de menú en la barra de navegación/inferior de la aplicación.
        * **Contenido:** Muestra tareas programadas en un formato de calendario (día, semana, mes, año).
        * **Navegación desde aquí:**
            * Volver a "Mi Universo de Tareas" (botón/gesto de retroceso).
            * Navegar a "Editar Tarea" (seleccionando una tarea directamente en el calendario).

    * **2.3. Vista "Seguimiento de Hábitos"**
        * **Acceso:** Desde un botón o elemento de menú en la barra de navegación/inferior de la aplicación.
        * **Contenido:** Muestra una lista de hábitos y su progreso de cumplimiento en un intervalo (ej. semanal).
        * **Navegación desde aquí:**
            * Volver a "Mi Universo de Tareas" (botón/gesto de retroceso).
            * Navegar a "Editar Tarea" (seleccionando un hábito para ajustar su configuración).

### 3. Vista "Crear/Editar Tarea" (Flujo Modal o de Detalle)

    * **Acceso:**
        * Desde la "Lista de Listas" (botón "Añadir Tarea" o seleccionar una tarea existente).
        * Desde la "Vista Calendario" (seleccionando un espacio de tiempo o una tarea existente).
        * Desde la "Vista Seguimiento de Hábitos" (seleccionando un hábito para editar).
    * **Contenido:** Formulario para introducir/modificar detalles de la tarea (título, descripción, fecha límite, prioridad, tipo de tarea, etc.).
    * **Navegación desde aquí:**
        * Volver a la vista de origen (Lista de Listas, Calendario, Seguimiento de Hábitos) al guardar o cancelar.

---