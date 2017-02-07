# HeapGUI

HeapGUI es una aplicación gráfica escrita utilizando el framework WPF
(Windows Presentation Foundation).

HeapGUI permite ver el funcionamiento de un MinHeap.

Un Heap es una estructura de datos que permite realizar en O(log(n)), siendo n
la cantidad de elemento que se encuentran en el heap, las siguientes operaciones:

-Obtener el mínimo elemento
-Insertar un elemento (encolar)
-Eliminar el mínimo elemento (desencolar)
-Eliminar un elemento al cual se tiene una referencia

Los heap pueden implementarse utilizando un array/vector o un árbol, MinHeapMod está
implementado sobre un árbol.


El usuario inserta elementos, en este caso numeros enteros, presionando el
boton Encolar y quita el mínimo elemento presionando el boton Desencolar.

En está versión el boton Eliminar está desabilitado ya que todavía no está
correctamente implementado.

Atención!
Esta versión contiene algunos bugs y en algunas secuencias de operaciones no 
reordena el árbol correctamente, lo cual se puede ver obsevando el mínimo.
