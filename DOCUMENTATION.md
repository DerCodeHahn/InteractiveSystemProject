# Interactive Systems Dokumentation
Christopher Hahn  
Timo Raschke  
Khaled Reguieg

# Inhaltsverzeichnis
1. [Projektbeschreibung](#desc)
2. [Projektziele](#goals)
3. [Projektverlauf](#runnning)
4. [Ausblick](#outlook)
5. [Fazit](#conclusion)

# Projektbeschreibung<a name="desc"></a>
Das Projektteam hat sich darauf verständigt, ein bestehendes Computerspiel mittels Machine Learning um eine intelligente künstliche Intelligenz zu erweitern. Zum Einsatz kommt die Technik *Unity Machine Learning Agents* (kurz ML-Agents) der Unity Engine.

## Unity Machine Learning Agents
Unity Machine Learning Agents ist ein quellenoffenes Plugin für Unity. Es stellt Spielen und Simulationen eine Trainingsumgebung für intelligente Agenten zu verfügung. Dabei kommen verschiedene Ansätze des machinellen Lernens zu Einsatz, darunter unter anderem:

* Reinforcement learning
* imitation learning

Eigene Ansätze können mittels der bereitgestellten Python-API realisiert werden.  
ML-Agents beinhaltet zusätzlich Implementierung von state-of-the-art Algorithmen, basierend aus TensorFlow.  
Mit Hilfe eines angelernten Agents können zum Beispiel Aktionen eines Nicht-Spieler-Charakters (eng. NPC) realisiert werden. Weitere verwendungsmöglichkeiten sind automatisiertes Testen oder das Testen von Änderungen im Design des Spiels.

## Beschreibung des Spiels
![Sea Brawl Logo](img/SeaBrawl.png "Sea Brawl")  
Das verwendete Spiel trägt den Namen "Sea Brawl" und stammt aus einen älteren Projekt aus dem Bachelorstudium. Auf einem abgegrenzten Terrain bekämpfen sich zwei oder mehr Schiffe. Ziel ist es, mittels Kanonenschüssen die Gegner zu treffen, und selbst den Kanonenkugeln gegnerischer Schiffe auszuweichen. Da das Spiel einen Wettkampf zwischen mehreren Spielern darstellt, eignet es sich sehr gut für einen Machine Learning Ansatz. Des Weiteren ist es bereits in Unity implementiert, wodurch sich die Anpassungen in Grenzen hielten.

# Projektziele<a name="goals"></a>
Ziel des Projekts ist es, mit Hilfe von Unity ML-Agents das Spiel *Sea Brawl* um einen KI-Modus zu erweitern. Dazu sollen die Möglichkeiten von Unity ML-Agents kennengelernt und die künstliche Intelligenz durch maschinelles Lernen erzeugt werden. Es gilt korrekte Belohnungsfunktionen für das vorhandene Spiel zu definieren, damit die Agenten nach dem Spielprinzip sinnvolle Aktionen ausführen. Dazu sollen die Ansätze von Reinforcement Learning und Imitation Learning verinnerlicht und angewandt werden. Dabei gilt es herauszufinden, welcher der Machine Learning Ansätze für das gegebene Spiel am besten geeignet ist.

## Minimal Viable Product
Das Team hat sich im Vorfeld aus ein Minimal Viable Product (kurz MVP) verständigt, welches für einen erfolgreichen Projektabschluss auf jeden Fall erfüllt sein sollte. Dieses sieht wie folgt aus:

* Zwei computergesteuerte Spieler spielen das Spiel selbstständig
* Spieler spielen das Spiel nach dem Spielprinzip und führen sinnvolle Aktionen aus
* Die Computerspieler sind nicht übermäßig stark, sodass kein menschlicher Spieler eine Chance hätte
* Die Computerspieler sind nicht zu schwach, sodass ein menschlicher Gegenspieler nicht unterfordert wird.

## Zusammenfassung der Aufgaben
* Sea Brawl an Unity ML-Agents anpassen
* Lernumgebung aufsetzen
  * Observation Space
  * Action Space
* Belohnungsfunktion definieren
* Hyperparameter definieren

# Projektverlauf<a name="running"></a>

## Entwicklungsumgebung<a name="environment"></a>
Die Entwicklungsumgebung besteht aus folgenden Komponenten:

* Windows, Mac OSX oder Linux
* Unity 2017.1 oder neuer
* Python 3
  * [TensorFlow](https://www.tensorflow.org/)
  * [Jupyter](http://jupyter.org/)

### Installation
Unitys ML-Agents Framework ist einfach einzurichten. Grundvorraussetzung ist das Erfüllen aller Abhängigkeiten aus der definierten [Entwicklungsumgebung](#environment).  
Auf Grund der Abgrenzung verschiedener Projekte wird der Einsatz von Python-Umgebungen empfohlen. Für das zu betrachtende Projekt wurde eine Anaconda Umgebung auf Basis von Python 3.6 verwendet.  
Die benötigten Python-Pakete sind in der `requirements.txt` im Unterverzeichnis `python/` des Repositories hinterlegt. Mittels `pip3 install .` können diese einfach installiert werden. Je nach Betriebssystem/Umgebungslösung kann sich der Name von `pip3` unterscheiden (z.B. nur `pip`).

## Anpassung von Sea Brawl
![SeaBrawl Remake in Unity aus der Vogelperspektive](img/seabrawl-top.png "SeaBrawl Remake in Unity aus der Vogelperspektive")
Nach eingehender Sichtung der Implementierung des Spiels Sea Brawl wurde schnell klar, das eine Anpassung des Spieles zu aufwendig wäre. Infolgedessen wurde das Grundprinzip des Spiels neu implementiert. Das Spiel besteht aus zwei Schiffen, die jeweils an der rechten und linken Seite einen Kanonenbug besitzen. Aus diesen können unabhängig von einander Kanonen abgefeuert werden. Die Schiffe befahren einen abgegrenzten Bereich, der Wasser darstellen soll. Bei der neu-Implementierung wurde von vornhinein darauf geachtet, dass das Spiel zu Unity ML-Agents kompatibel ist. Auf aufwendige grafische Effekte wurde zu gunsten der Zielerfüllung des Projekts verzichtet.

## Near-Area der Schiffe
Die Schiffe wurden zusätzlich zu den zuvor genannten Funktionen um jeweils zwei Near-Areas erweitert. Dies sind definierte Bereiche, die sich kreisförmig um die Schiffe bilden. Jedes Schiff hat je einen kleinen und einer weitläufigeren Bereich um seinen Körper. Diese Bereiche sind im späteren Verlauf für die Berechnung von Belohnungen von Nöten. So kann berechnet werdem, ob ein Kanonenschuss eines Spielers in die Nähe des gegenerischen Schiffs gelangt. Die zwei Bereiche wurden definiert, um eine Abstufung zu erreichen (in der Nähe und sehr nah). Für den Betrachter sind die Near-Areas nicht sichtbar.

## Aufsetzen der Lernumgebung
![Learning Environment](docs/images/learning_environment.png "Learning Environment")

## Implementierung

## Belohnungsfunktionen

## Ergebnisse

# Ausblick<a name="outlook"></a>

# Fazit<a name="conclusion"></a>
