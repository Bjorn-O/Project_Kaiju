# Project_Kaiju


# Geproduceerde Game Onderdelen


Riley:
* [overheat mechanic](https://github.com/Bjorn-O/Project_Kaiju/tree/Develop/Unity%20Project-%20Kaiju/Assets/Scripts/Gameplay/Ammo)
* [audio systeem](https://github.com/Bjorn-O/Project_Kaiju/tree/Develop/Unity%20Project-%20Kaiju/Assets/Scripts/Systems/Audio)
* [Health system](https://github.com/Bjorn-O/Project_Kaiju/tree/Develop/Unity%20Project-%20Kaiju/Assets/Scripts/Gameplay/Health)

# overheat mechanic

De overheat feature zorgt er voor dat de game gebalanced door de player niet oneindig te laten schieten
Om het telaten werken voeg je het script toe aan de gun en in de shooting script moet bij de shooting function de fire function hebben ook zijn er 2 events die er invooked worden wanneer hij overheated is en wanneer hij weer afgekoeld is je lan ook in de inspecter de floats van de heatthreshold aanpassen om het spel anders te blanceren


### flowchart voor overheat mechanic:
```mermaid
graph TD;

Start((Start)) --> A{Is firing?};
A --> |Yes| B{Is overheated?};
B --> |Yes| C[Cooldown];
B --> |No| D{Is heat above threshold?};
C --> D;
D --> |Yes| E[Overheated];
D --> |No| A;
E --> F[Cooling down];
F --> B;

```


# Audio system

De audio systeem zorgt voor alle audio in de game
Je kan in de inspecter audioclips in de array van sounds op de plus klikken je kan dan een clip er aan toevoegen een naam geven en het volume van de clip aanpassen
1.	De sounds array stamt af van een costum class genaamd Sound die hoef je nergens aan toe te voegen, die script zorgt voor de array van clips zodat ze allemaal een naam krijgen en andere vraiable kan aanpassen

2.	audiomanager script toevoegen aan een gameobject. om een sound af te laten spelen moet je in een ander script waar je het geluidje van wil laten afspelen de Play(string name) functie aanroepen en de parameter moet de naam zijn van de sound die je hebt toegevoegd dus niet de audioclips naam zelf maar de naam die je hebt toegevoegd.



### flowchart voor Audio manager:
```mermaid
flowchart TD
    A(Start Execution) --> B(Declare sounds)
    B --> C(On Awake )
    C --> D{Loop through<br>sounds array}
    D --> E[Add AudioSource component]
    E --> F[Set AudioClip<br>for AudioSource]
    F --> G[Set volume<br>for AudioSource]
    G --> H[Set pitch<br>for AudioSource]
    H --> I[Set loop<br>for AudioSource]
    J(On Play paramater: string name)
    J --> K[Find sound by<br>name in array]
    K --> L{Check if sound<br>exists}
    L --> M[Print warning<br>if sound not found]
    L --> N[Play audio]
    N --> O(End Execution)


```


# Health system

Het gezondheidssysteem biedt functionaliteit om de gezondheid van spelers en vijanden in een spel te beheren. Het stelt je in staat om gezondheidswaarden bij te houden en bij te werken, events te activeren bij veranderingen in de gezondheid de health system bestaat uit 3 scripts: playerhealth enemy health en IDamage
de health scripts voor enemy en player houden hun currenthealth bij en de Idamage in een interface die een takedamage functie heeft met een damage parameter


### flowchart voor overheat mechanic:
```mermaid

graph TD
    A[Player/Enemy Health] -->B(Take Damage)
    B -->|Check Health Zero| C((Game Over))

```
