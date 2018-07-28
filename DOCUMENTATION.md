# Interactive Systems Dokumentation
Christopher Hahn  
Timo Raschke  
Khaled Reguieg

# Inhaltsverzeichnis
<!-- toc -->


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

### Near-Area der Schiffe
<img src="img/neararea1.png" alt="Nahe Near-Area" title="Nahe Near-Area" width=440><img src="img/neararea2.png" alt="Weite Near-Area" title="Weite Near-Area" width=440px>  
Die Schiffe wurden zusätzlich zu den zuvor genannten Funktionen um jeweils zwei Near-Areas erweitert. Dies sind definierte Bereiche, die sich kreisförmig um die Schiffe bilden. Jedes Schiff hat je einen kleinen und einer weitläufigeren Bereich um seinen Körper. Diese Bereiche sind im späteren Verlauf für die Berechnung von Belohnungen von Nöten. So kann berechnet werdem, ob ein Kanonenschuss eines Spielers in die Nähe des gegenerischen Schiffs gelangt. Die zwei Bereiche wurden definiert, um eine Abstufung zu erreichen (in der Nähe und sehr nah). Für den Betrachter sind die Near-Areas nicht sichtbar.

## Learning Environment
![Learning Environment](docs/images/learning_environment.png "Learning Environment")  
In Unity ist eine Learning Environment (dt. Lernumgebung) die Grundvorraussetzung, um mit ML-Agents arbeiten zu können. Grundlegend wird eine Learning Environment als eine Applikation beschrieben, die auf der Unity Engine fußt. Demzufolge ist SeaBrawl ebenfalls eine Learning Environment. Weitere Bestandteile sind die Academy, Brains und Agents. Diese werden im Folgenden näher erläutert.

### Agents
Ein Agent beobachtet seine Umwelt und kann auf Grundlage dieser Beobachtungen den besten Weg zu einer Lösung finden. Ein Agent muss mit einem Brain verbunden sein und gibt seine Beobachtungen an dieses weiter Er erhält von diesem eine Entscheidung zurück. In ML-Agents existieren drei Möglichkeiten der Beobachtung:

* Kontinuierlicher Vektor - ein Feature Vektor, der aus einem Array von Zahlen besteht
* Diskreter Vektor - ein Index in einer Zustandstabelle (nur für sehr einfache Umgebungen)
* Visuelle Beobachtungen - ein oder mehrere Kamerabilder

### Brains
Ein Brain ist für den Entscheidungsprozess zuständig. Das Brain entscheidet anhand der Beobachtungen eines Agents und gibt diese Entscheidung an ihn zurück. Ein Brain kann von einem oder mehreren Agents verwendet werden. In ML-Agents existieren vier Typen von Brains:

* External - Zum trainieren von Agents (zum Beispiel via Python)
* Internal - Verwenden eines trainierten Models
* Heuristic - Hartkodierte Logik (klassischer Ansatz, kein Machine Learning)
* Player - Menschlicher Spieler (zum Beispiel um den Agent zu testen)

### Academy
Eine Academy orchestriert alle Agents und Brains einer Unity-Szene. Jede Szene, die Agents enthält muss zwingend eine Academy besitzen. Eine Academy wird für folgende Funktionen verwendet:

* Initialisieren der Umgebung nachdem die Szene geladen wurde
* Zurücksetzen der Umgebung
* Verändern von Dingen in der Umgebung nach jeden Simulationsschritt

## Observation Space
![Observation Space](img/ObservationSpace.png "Observation Space")  
Für den Observation Space (die Dinge, die ein Agent beobachten kann) wurden folgende Attribute gewählt:

| Typ       | Beschreibung                         |
|-----------|--------------------------------------|
| `Vector2` | Richtung zum Schiff                  |
| `Float`   | Entfernung zum Schiff                |
| `Vector2` | Bewegungsrichtung des Schiffs        |
| `Vector2` | Richtung der Nächsten Kanonenkugel   |
| `Float`   | Entfernung der nächsten Kanonenkugel |
| `Vector2` | Bewegungsrichtung der Kanonenkugel   |

## Action Space
Der Action Space definiert die Aktionen, die ein Brain einem Agent als Entscheidung zurück geben kann. Folgende Actions wurden definiert:
* Horizontales Fahren `(W , S)`
* Vertikales Fahren `(A, D)`
* Schießen einer Kanonenkugel aus dem linken oder rechten Schiffsbug `(C, V)`

## Belohnungsfunktionen
![Belohnungsfunktion](img/rewardfunction.png "Belohnungsfunktion")
Anhand einer Belohnungsfunktion kann ein Brain bewerten wie gut die an den Agent gegebene Entscheidung war. Es wurden drei Belohnungsfunktionen definiert:

| Aktion                                                            | Belohnungswert |
|-------------------------------------------------------------------|---------------:|
| Treffen des Gegners                                               |           =1.0 |
| Abgeschossene Kanonenkugel verfehlt Ziel knapp (äußere Near-Area) |          +0.01 |
| Abgeschossene Kanonenkugel verfehlt Ziel knapp (innere Near-Area) |           +0.1 |
| Schiff fährt gegen eine Wand                                      |          -0.01 |

## Ergebnisse

### Wie es aussehen sollte
![Beispielhafte Graphen aus Tensorboard](docs/images/mlagents-TensorBoard.png "Beispielhafte Graphen aus Tensorboard")  

### Extern vs extern mit einem gemeinsamen Brain
![Graphen aus Tensorboard](img/Extern-vs-Extern-1-Brain.png "Graphen aus Tensorboard")  

### Extern vs extern mit einem gemeinsamen Brain (Nachttraining)
![Graphen aus Tensorboard](img/Extern-vs-Extern-1-Brain-Nightly.png "Graphen aus Tensorboard")  

### Extern vs extern mit je einem Brain
![Graphen aus Tensorboard](img/Extern-vs-Extern-2-Brain.png "Graphen aus Tensorboard")  

### Sonderfälle
![Im Kreis fahrende Schiffe](img/learning.gif "Im Kreis fahrende Schiffe")  

### Ergebnisse gegen eine Heuristik

#### Funktionen der Heuristik
1. Ausweichen
2. Schießen
3. Verfolgen

→ Sehr stark  
Noch nicht geschafft mit einem KI-Agent die Heuristic zu besiegen.

#### Ergebnisse gegen die Heuristik
| Gegner                                       | Ergebnis (win/loss/draw) |
|----------------------------------------------|--------------------------|
| External vs External 1 Brain (20 Min)        | 38-151-11                |
| External vs External 2 Brain (20 Min) Brain1 | 41-149-10                |
| External vs External 2 Brain (20 Min) Brain2 | 26-153-21                |
| Spieler                                      | 33-153-14                |
| IL Internal (20 Min)                         | 34-157-9                 |
| External 1 Night Learn x16 (6 hours)         | 60-140-0                 |

# Ausblick<a name="outlook"></a>
* Imitation Learning gegen die Heuristic
* External Learning  gegen die Heuristic
* Längere Learn-Sessions
* Hyperparameter

# Fazit<a name="conclusion"></a>
* Heuristik Eignet sich gut um Observation Space und Action Space zu Evaluieren
* `--load` lässt Training weiter laufen
* Unity-ML Agents leider noch im preview mode
