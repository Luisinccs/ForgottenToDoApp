# ForgottenToDoApp
## Especificación de Requerimientos de Software (SRS) - 
**Fecha:** 2025-06-09

---

## 1. Introducción

### 1.1 Propósito
Este documento describe los requisitos funcionales y no funcionales de la aplicación "ForgottenToDoApp". El propósito principal de esta aplicación es proporcionar al usuario una "brújula" y un "ancla" para organizar y priorizar sus tareas y proyectos. Busca transformar una lista de tareas en un "mapa mental" interactivo y visualmente navegable.

### 1.2 Alcance
ForgottenToDoApp será una aplicación de gestión de tareas y proyectos, con un enfoque innovador en la visualización gráfica de la organización de las responsabilidades del usuario. Permitirá categorizar tareas, gestionar diferentes tipos de repetición y navegar por un "universo de tareas" de alto nivel.

### 1.3 Audiencia
La audiencia principal de este documento incluye al equipo de desarrollo, diseñadores de UI/UX, testers y cualquier stakeholder interesado en la funcionalidad y arquitectura de ForgottenToDoApp.

---

## 2. Descripción General

### 2.1 Visión del Producto
ForgottenToDoApp tiene como objetivo principal resolver el problema de la desorganización y la falta de enfoque en la gestión de múltiples proyectos y tareas personales. A través de una interfaz de mapa mental interactiva y una separación clara de las "Áreas de Enfoque", la aplicación permitirá a los usuarios tener una visión clara de sus compromisos y el siguiente paso a seguir.

### 2.2 Usuarios
* **Usuario Individual:** Persona que necesita organizar y priorizar sus proyectos profesionales, tareas personales, estudios y otros compromisos.

### 2.3 Restricciones
* **Arquitectura:** La aplicación debe seguir una arquitectura desacoplada, con el Core (ViewModels e interfaces) separado de la capa de UI.
* **Tecnología UI:** La primera implementación de la interfaz de usuario será en .NET MAUI.
* **Dependencia de Vistas:** Las vistas (UI) solo deben tener referencia a las interfaces de los ViewModels (`IViewModel`), no a las implementaciones concretas.
* **Comandos:** El uso de `ICommand` en los ViewModels debe ser agnóstico de plataforma (`System.Windows.Input.ICommand` que es parte estándar de .NET). Las implementaciones (`RelayCommand`, `AsyncRelayCommand`) provendrán de `CommunityToolkit.Mvvm`.
* **Formato de Código:** Adherencia estricta a las convenciones de estilo de C# definidas (propiedades en PascalCase sin underscore, comentarios `/// <summary>` en una sola línea, un tipo por archivo, directivas en orden alfabético, etc.).
* **Pruebas de UI:** Se implementará un proyecto de pruebas de UI (`ForgottenToDoApp.Views.Maui.Tests`) que actúe como host para cargar y probar las vistas individualmente, utilizando mocks para los ViewModels.

---

## 3. Requisitos Funcionales

### 3.1 Gestión de Proyectos y Tareas (General)
* La aplicación permitirá la creación, edición y eliminación de tareas y proyectos.
* Las tareas se agruparán dentro de "Áreas de Enfoque" y "Sub-Áreas de Enfoque".

### 3.2 Tipos de Tareas
La aplicación debe distinguir y permitir la gestión de los siguientes tipos de tareas:
* **Tareas Únicas:** Actividades que se realizan una sola vez.
* **Tareas Repetitivas No Acumulativas:** Tareas periódicas que, si no se realizan un día, no se arrastran para el siguiente (ej. ejercicio).
* **Tareas Repetitivas Acumulativas:** Tareas periódicas que, si no se realizan, sus efectos o la tarea en sí se acumulan para el siguiente período (ej. pagos de servicios).
* **Tareas Comunes:** Tareas que son aplicables a múltiples proyectos o áreas de enfoque (ej. "Averiguar estrategias de monetización de un blog" que podría aplicar a varios proyectos de blogs).

### 3.3 Vista "Mi Universo de Tareas" (Mapa de Enfoque)
* **RF.3.3.1 - Visualización Centralizada:** La aplicación debe mostrar una vista principal, denominada "Mi Universo de Tareas", que represente gráficamente todos los proyectos y tareas agrupadas.
* **RF.3.3.2 - Nodos Interactivos:** El "Universo de Tareas" se representará como un nodo central. Desde este nodo se extenderán "Áreas de Enfoque" principales (ej. "Proyectos", "Aprendizaje", "Tareas Personales").
* **RF.3.3.3 - Ramificación Jerárquica:** Cada "Área de Enfoque" podrá tener "Sub-Áreas de Enfoque" anidadas, formando una estructura de ramas y nodos.
* **RF.3.3.4 - Edición Gráfica de Nodos:** El usuario debe poder añadir, editar y eliminar "Áreas de Enfoque" y "Sub-Áreas de Enfoque" directamente desde la interfaz gráfica del mapa.
* **RF.3.3.5 - Navegación por el Mapa:** Al tocar/seleccionar un nodo de "Área de Enfoque" o "Sub-Área de Enfoque", la vista debe cambiar a una "Lista de Listas" que muestre las tareas y sub-áreas asociadas a ese nodo específico.
* **RF.3.3.6 - Representación de Tareas Comunes:** Las "Tareas Comunes" serán un concepto que se gestionará en el nivel del ViewModel, y su representación visual en el mapa se realizará mediante líneas que conecten las Áreas/Sub-Áreas relevantes. (Las tareas individuales no se muestran en el mapa de alto nivel).

### 3.4 Vista "Lista de Listas"
* **RF.3.4.1 - Detalle del Área de Enfoque:** Al seleccionar un nodo en el mapa, la aplicación navegará a una vista de lista que muestra las tareas directamente asociadas a esa Área/Sub-Área y/o las sub-áreas anidadas.
* **RF.3.4.2 - Gestión de Tareas en Lista:** Dentro de esta vista, el usuario podrá ver el estado, editar detalles y marcar como completadas las tareas.

### 3.5 Vista "Calendario"
* **RF.3.5.1 - Detalle del Área de Enfoque:** La aplicación debe mostrar una vista de calendario, la cual podrá ser especificada en dia, semana, mes o año.
* **RF.3.5.2 - Gestión de Tareas en Calendario:** Dentro de esta vista, el usuario podrá ver el estado, editar detalles y marcar como completadas las tareas.

### 3.6 Vista "Seguimiento de hábitos"
* **RF.3.6.1 - Lista de hábitos en un intervalo:** La aplicación deberá mostrar una lista rápida de aquellas actividades establecidas como hábitos; prefereblemente en un plazo de una semana, de modo que el usuario pueda ver los hábitos de ese plazo marcar los que ya ha completado y ver su avance.

---

## 4. Requisitos No Funcionales

### 4.1 Rendimiento
* **RNF.4.1.1 - Fluidez de la UI:** La interfaz de usuario, especialmente el mapa mental interactivo, debe responder fluidamente a las interacciones del usuario, sin retrasos perceptibles.
* **RNF.4.1.2 - Carga Rápida:** La aplicación y sus vistas deben cargar rápidamente.

### 4.2 Usabilidad
* **RNF.4.2.1 - Intuitividad:** La interfaz debe ser intuitiva y fácil de usar, permitiendo a los usuarios comprender rápidamente cómo organizar y navegar por sus tareas.
* **RNF.4.2.2 - Claridad Visual:** El mapa mental debe ser visualmente claro, utilizando colores y formas de manera efectiva para diferenciar las "Áreas de Enfoque".

### 4.3 Mantenibilidad
* **RNF.4.3.1 - Desacoplamiento:** La arquitectura de la aplicación debe garantizar un alto grado de desacoplamiento entre las capas Core (lógica de negocio y ViewModels) y UI (vistas).
* **RNF.4.3.2 - Modularidad:** El código debe estar modularizado en proyectos (`.Core`, `.Data`, `.Views.Maui`, `.Views.Maui.Tests`, `.App.Maui`) con responsabilidades claras.

### 4.4 Testabilidad
* **RNF.4.4.1 - Pruebas Unitarias de ViewModel:** Los ViewModels deben ser completamente testeables de forma unitaria, sin depender de la UI.
* **RNF.4.4.2 - Pruebas de UI Aisladas:** Las vistas MAUI deben poder probarse de forma aislada utilizando ViewModels mock/dummy, sin depender del contexto completo de la aplicación.

### 4.5 Portabilidad
* **RNF.4.5.1 - Multiplataforma:** La aplicación debe ser capaz de ejecutarse en las plataformas soportadas por .NET MAUI (Windows, Android, iOS, macOS).

### 4.6 Escalabilidad
* **RNF.4.6.1 - Manejo de Datos:** El diseño de la aplicación debe permitir un manejo eficiente de un creciente número de proyectos, áreas de enfoque y tareas sin degradación significativa del rendimiento.

---