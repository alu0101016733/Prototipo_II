#Prototipo_II

## Descripción
Nuestro juego space drone consiste en disparar a drones que tienen un movimiento que sigue un camino aleatorio para eliminarlos. El juego cuenta con varios niveles en los que el número de drones y su velocidad va aumentando. Se ha tenido en cuenta el hecho de que se desarrolla para realidad virtual, haciendo uso de una mira, poniendo un fondo 360 grados y haciendo que el usuario no tenga que realizar movimientos bruscos de la cabeza para evitar mareos.

## Cuestiones importantes para su uso
Tener en cuenta que este juego ha sido desarrollado para realidad virtual en android. Se necesita un mando con bluetooth para poder controlarlo en android, sin embargo funciona con teclado y ratón en Windows.

## Hitos de programación logrados relacionándolos con los contenidos que se han impartido
###Interacción entre objetos. Eventos
- Las balas al impactar con algún enemigo le quitan vida a este y si la vida les baja a cero explotan y se destruyen. Además cuando la bala - impacta se hace notar con unas partículas de impacto en el enemigo.
- Si el usuario realiza algún sonido fuerte los enemigos se lanzaran asi él y explota
- Existe un game controller que se comunica con varios objetos del juego y que los mismos se comunican con él. Por ejemplo, el game  controller puede decirle al menú de pausa que pare o continúe el juego, los enemigos notifican de su muerte, el game controller controla el número de enemigos y el nivel,etc
- Usamos linecast para notificar cuando atraviesan las balas al enemigo
- Utilizamos el nuevo sistema de input de Unity para mapear los botones del mando (además del teclado) para poder controlarlo en la versión android
### Multimodalidad
- Existe para la versión de Windows un reconocedor de palabras clave por voz. Podemos pausar y continuar el juego usando las palabras clave: Pause y Resume.
- Incorporamos un gps que nos indica la localización en la que estemos situados, podemos verla debajo del título del juego en el menú de pausa.
- Si el usuario hace un sonido fuerte los drones vienen hacia el usuario, esto también se consigue usando el microfono.
- Uso de sensores por parte de la integración con Google Cardboard, también se recogen los datos en un script para posibles nuevas funcionalidades.

### Aspectos destacables de la implementación
Sin duda lo más destacable y complejo ha sido la implementación de los caminos a seguir por los drones. Se ha utilizado BezierCurves y CatmullRomSpline. Además la comunicación a través de varios ficheros teniendo en cuenta la cantidad de los mismos ha sido compleja.

## Descripción de archivos
- User input implementa las funciones del menú y escucha tanto a los botones del mando como a las teclas del teclado como a los comandos del game controller.
- User input definition especifica que valores se asocian a que acción
- Gun se encarga de disparar es decir de crear las instancias de las balas y asignarles un determinado daño
- Bullet contiene las especificaciones necesarias de la bala daño, posición, velocidad.
- Gamecontroller establece el estado del juego y se comunica con el menú
- playercontrols es el fichero génerado por Unity input en el que se establece el mapeado de botones y teclas
- DrawGizmoForCatmullRomSpline se encarga de dibujar (gizmo) las líneas de la trayectoria del catmullromspline
- DrawGizmoForBezierCurve se encarga de dibujar (gizmo) las líneas de la trayectoria del bezier curve
- BasicUtilitiesForAllScripts
- BezierCurveUtilities
- CatmullRomSplineUtilities
- CatmullSplines
- MoveAlongBezierCurve
- MoveAlongCatmullRomSpline
- GameObjectSearcher clase como utilidad para encontrar un gameobject especifico que necesitemos
## Acta de trabajo en equipo
### Thaddaus
- Plataforma donde se mueve el usuario
- Implementación del micrófono
- Movimiento del enemigo hacia el usuario
- Camino a seguir por el enemigo
- Game controller
- Implementación GPS
### Adrián
- User input métodos
- Imagen 360 para el diseño y el horizonte
- Sonidos del juego
- Arma del usuario
- Balas
### Equipo
- Reconocedor de palabras clave
- Drones (enemigos)
- Usuario (cámera) con Google Cardboard
- Interfaz de usuario
Hemos utilizado github projects para organizar las tareas y hemos trabajo por las tardes comunicandonos con google meet.

alt-img

### Gif animado de ejecución