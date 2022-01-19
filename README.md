# PresayCasador
Utilización del  algoritmo Dijkstra y C#
Proyecto para la materia de seminario de algoritmia
Presas.
Entidad que busca llegar a un vértice objetivo. Pueden existir múltiples presas en el mismo
entorno, si no existe un objetivo en el grafo, las presas permanecen en un vértice.
La presa conoce todo el grafo e intentará dirigirse al objetivo utilizando el camino más corto, pero
si se ve asechada, cambiará su comportamiento para su salvaguarda. La presa puede tomar
decisiones de hacia donde dirigirse en cualquier instante, incluso mientras recorre una arista.
La presa sabe cuando un depredador la está acechando, conoce al depredador, de donde viene y
hacia donde se dirige en cada instante. Si una presa es alcanzada por un depredados la presa
desaparece.

Depredadores.
El objetivo de los depredadores es alcanzar a una presa, conocen a las presas que se encuentran a
un máximo de r (realizar pruebas con 300) metros a la redonda de él, pero solo pueden acechar a
una, siempre y cuando no esté siendo acechada por otro depredador. Si un depredador no detecta
presas en su radio, éste recorre el grafo de forma aleatoria. Puede haber múltiples depredadores
en el entorno.
Un depredador solo puede tener una presa en asecho. El depredador conoce el destino (vértice al
que se dirije) de la presa en acecho. El depredador puede cambiar de presa acechada en cualquier
momento.

Cuando un depredador toma la decisión de recorrer una arista, debe terminar de recorrerla para
tomar una nueva decisión. El depredador no puede elegir quedarse en un vértice, durante toda la
simulación, el depredador esta recorriendo aristas.
Objetivo.
El objetivo es una partícula más, colocada en un vértice específico y es inmóvil. Las presas generan,
a partir de donde se encuentran, caminos óptimos para llegar al objetivo. Cuando una presa llega
al vértice donde se colocó al objetivo, el objetivo desaparece.
