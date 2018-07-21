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
Unity Machine Learning Agents ist ein quellenoffenes Plugin für Unity. Es stellt Spielen und Simulationen eine Trainingsumgebung für intelligente Agenten zu verfügung. Dabei kommen verschiedene Ansätze des machinellen Lernens zu Einsatz, darunter unter anderem

* Reinforcement learning
* imitation learning
* neuroevolution

Eigene Ansätze können mittels der bereitgestellten Python-API realisiert werden.  
ML-Agents beinhaltet zusätzlich Implementierung von state-of-the-art Algorithmen, basierend aus TensorFlow.  
Mit Hilfe eines angelernten Agents können zum Beispiel Aktionen eines Nicht-Spieler-Charakters (eng. NPC) realisiert werden. Weitere verwendungsmöglichkeiten sind automatisiertes Testen oder das Testen von Änderungen im Design des Spiels.

## Beschreibung des Spiels
![Sea Brawl Logo](img/SeaBrawl.png "Sea Brawl")  
Das verwendete Spiel trägt den Namen "Sea Brawl" und stammt aus einen älteren Projekt aus dem Bachelorstudium. Auf einem abgegrenzten Terrain bekämpfen sich zwei oder mehr Schiffe. Ziel ist es, mittels Kanonenschüssen die Gegner zu treffen, und selbst den Kanonenkugeln gegnerischer Schiffe auszuweichen. Da das Spiel einen Wettkampf zwischen mehreren Spielern darstellt, eignet es sich sehr gut für einen Machine Learning Ansatz. Des Weiteren ist es bereits in Unity implementiert, wodurch sich die Anpassungen in Grenzen hielten.

# Projektziele<a name="goals"></a>
Ziel des Projekts ist es, mit Hilfe von Unity ML-Agents das Spiel *Sea Brawl* um einen KI-Modus zu erweitern. Die künstliche Intelligenz soll durch maschinelles Lernen erzeugt werden. Es gilt korrekte Belohnungsfunktionen für das vorhandene Spiel zu definieren, damit die Agenten nach dem Spielprinzip sinnvolle Aktionen ausführen.

# Projektverlauf<a name="running"></a>

## Konzept

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

## Implementierung

## Belohnungsfunktionen

## Ergebnisse

# Ausblick<a name="outlook"></a>

# Fazit<a name="conclusion"></a>
