# UnityBowSystem

## Conceito inicial:
- Foi um projeto antigo onde eu estava testando uma ideia, se eu poderia gerenciar dinamicamente a instancia de itens no cenário.

> Propósito:
- Criar uma mecânica simples de instanciamento de um arco a um personagem.

## Recursos:
- O script "" permite que o personagem gerencie equipando e trocando o arco quando necessário.
- Possui uma flecha que ao atingir o alvo consegue se fixar a ele, mesmo que ele se mova ela permanecerá no local.

## Organização dos arquivos:

![Screenshot_4](https://user-images.githubusercontent.com/37397920/219293625-87a4e776-1cc7-4b15-aeea-6db7b59314ae.png)


## Modo de uso:
Obs: um conceito importante, existe um elemento usado no código chamado de ancora (anchor) esses objetos são "Transform" e servem de referencia para as instancias do arco e flecha, ou seja eles serão instanciados dentro destes objetos. As ancoras estão localizadas no objeto do personagem, se o personagem possuir rig recomendo colocar a ancora dentro do osso desejado. 

1º - As funções usadas para controlar a exibição tanto do arco/crossbow e da flecha são executadas através de eventos de animação (Animation Events), podem ser implementados nas animações, ou em qualquer script que possua a referencia destes scripts, abaixo serão exibidos os scripts todos estes componentes são atribuidos ao prefab do personagem:
-> Script "BowCtrl": É usado para controlar o arco ou crossbow.

![Screenshot_2](https://user-images.githubusercontent.com/37397920/219285342-69350a62-a6d7-4149-b11a-adea39819d54.png)

Possui funções executadas em "Animation Events":
- "TakeBow" -> usado para que o personagem ative "inHand" e exciba o arco em sua mão.
- "KeepBow" -> usado para que o personagem desative "inHand" e exciba o arco em suas costas, desequipado.

-> Script "ArrowCtrl": É usado para controlar a exibição da flecha, recebe a referencia do script "BowCtrl", este script precisa da referencia para funcionar.

![Screenshot_1](https://user-images.githubusercontent.com/37397920/219280192-85cc836a-65b0-4a7b-8d3f-adee530a1fed.png)

Possui funções executadas em "Animation Events":
- "ArrowInHandTrue" e "ArrowInHandFalse"-> Ativa/Desativa a exibição da flecha na mão do personagem e passa o objeto da flecha para a ancora o arco ou besta (arrow ou crossbow).
- "ArrowHideTrue" e "ArrowHideFalse" -> Ativa/Desativa a renderização da flecha deixando ela oculta. 

2º - A flecha possi alguns componentes:
- Script "Arrow Info": Serve para desaticar/ativar a renderização da flecha. Possui uma lista que você pode colocar todos os objetos de renderização, "Mesh Renderer" etc, ligados a flecha.
- Script "ArrowCollision": Trata da colisão da flecha, segue a imagem:

![Screenshot_3](https://user-images.githubusercontent.com/37397920/219290596-5229386b-dbf8-4636-b0a3-28febb65b6f5.png)

- Script "ArrowGrip": Script responsável por fazer a flecha se fixar no alvo após a colisão.

Obs: A flecha possui também um "Rigidbody", sem "gravidade", marcado com "isKinematic" igual a verdadeiro.

3º - O bow/crossbow possui um único script:
- Script "BowInfo" -> contém a força do disparo, a ancora onde a flecha fica quando a arma é recarregada e uma lista de componentes "Renderer" que serão desativados e ativados, quando for requisitado.

### Obs: Em caso de dúvida consulte a demo, ela possui uma cena com todos os recursos em uso, use as descrições dos scripts neste documento como um mapa.

###### O projeto é livre para download e uso, espero que goste e aprecie minha evolução.
