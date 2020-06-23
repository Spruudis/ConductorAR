# ConductorAR
Below, we have provided a descriptive run-down of the file structure of the Assets within the ConductorAR unity project. This is the folder where any continued work will be implemented by further developers. I would recommend opening this project in Unity and using this README document as a reference to explore the document. As this project has been built using the Unity Engine, any exploration of the documents and parts of this project are easier when done through the Unity Editor, with scripts best viewed in a text editor or C# IDE.

## Assets File structure
### -- AR
#### ---- Button Prefabs
This folder contains the button images and prefabs used for the buttons within the game. For example, the instrument selection buttons and the back and pause buttons.  
#### ---- Instrument Prefabs
In this folder we find the Instrument Placers folder, and the prefabs for the Instrument Avatars. These are the avatars involved in the actual game. So far, only Cello, Piano, Strings and Synth avatars have been made. Each of the avatars have an Animator Component, an Audio Source Component, and the scripts InstrumentControl and AnimationControl attached. These scripts control the interaction with the avatar and the animations the avatar performs. The Animator Component references the animator state machine attributed to the specific avatar, whereas  the Audio Source Component handles the music played by the avatar.      
##### ------ Instrument Placers
These are the prefabs for the instrument avatars before they're placed, e.g. they are a placement indicator. These placers correspond to all of the existing Instrument prefabs: Cello, Piano, Strings and Synth.
#### ---- Models
This folder contains the models, materials, and textures imported and used to make the instrument avatars.
#### ---- Scenes
This folder contains the Orchestra Scene.
- AR session Origin: Contains the AR camera used to 3d model an AR object on the user's device.
- AR session: handles all AR mechanics and control
- Placement Indicator: The game object in the scene that indicates where an instrument avatar will be placed. Its children are comprised of the instrument placers that can be set active and become the acting placement indicator icon.
- ObjectSpawner: A placeholder for the ObjectSpawner script
- ObjectManager: A placeholder for the ObjectManager script
- AR default plane: Contains AR foundation scripts and functionality that finds a plane for the user to place AR objects onto.
- Canvas: The children of this object are essential. They are the buttons the user interacts with to spawn an object, to pause the game, to back out of the game etc. All of these children contin scripts that control their functionality. 
#### ---- Scripts
This folder contains the scripts used for the AR component of the game:
- BackButton: The BackButton script assigns functionality to the back button in the Orchestra scene
- ButtonManager: The ButtonManager script manages all buttons present for instrument selection in the Orchestra scene.
- InstrumentControl: The InstrumentControl script controls the functionality of the audio source component of the instrument avatar.                          Furthermore, it handles the cueing of the avatar and which stem clip it plays.
- LongPressButton: The LongPressButton script controls the functionality of detecting if a user is holding down an instrument selection                    button.
- ObjectManager: The ObjectManager script is attached to the ObjectManager within the Orchestra Scene. This script holds references to the instrument avatars that need to be spawned, whether they have been spawned, etc.
- ObjectSpawner: The ObjectSpawner script is attached to the ObjectSpawner Orchestra Scene and handles the functionality such as                          spawning the objects, despawning the objects, and priming the instrument avatars for the start of the game.
- OnTouchDown: OnTouchDown is active during the actual game, and tracks user touches. These touches are then used to activate the OnTap function on the corresponding instrument avatar that the user tapped on.
- PauseManager: Manages all objects attributed to the pause button and the pause mechanics for the game.
- PlacementIndicator: Uses raytracing to track position of the placement indicator for augmented reality placement. Furthermore, works with instrument selection buttons to change the placement indicator icon to the correct instrument placer avatar.
- SpawnButton: Manages functionality of the spawn buttons. 
### -- Animations
#### ---- CelloAnimations
This folder contains the animations created for the Cello Instrument Avatar. Also, there is the Cello Animation Controller. This is a state machine that switches between animations when provided with specific triggers. For example, if the instrument avatar is in a playing animation but then triggered with 'Idle', then the instrument avatar switches to the idle animation. These triggers are handled by the AnimationControl script and this script is used by InstrumentControl.
#### ---- PianoAnimations
This folder contains the animations created for the Piano Instrument Avatar. Also, there is the Piano Animation Controller. This is a state machine that switches between animations when provided with specific triggers. For example, if the instrument avatar is in a playing animation but then triggered with 'Idle', then the instrument avatar switches to the idle animation. These triggers are handled by the AnimationControl script and this script is used by InstrumentControl.
#### ---- StringsAnimations
This folder contains the animations created for the Strings Instrument Avatar. Also, there is the Strings Animation Controller. This is a state machine that switches between animations when provided with specific triggers. For example, if the instrument avatar is in a playing animation but then triggered with 'Idle', then the instrument avatar switches to the idle animation. These triggers are handled by the AnimationControl script and this script is used by InstrumentControl.
#### ---- SynthAnimations
This folder contains the animations created for the Synth Instrument Avatar. Also, there is the Synth Animation Controller. This is a state machine that switches between animations when provided with specific triggers. For example, if the instrument avatar is in a playing animation but then triggered with 'Idle', then the instrument avatar switches to the idle animation. These triggers are handled by the AnimationControl script and this script is used by InstrumentControl.
### -- Menus
#### ---- Fonts
Folder containing fonts used in the project.
#### ---- Instrument Icons
Folder containing instrument icons used for the buttons in the SongSelection Scene.
#### ---- Scenes
Folder containing the Scenes for the initial app Menus.
#### ---- Scripts
Folder containing the scripts controlling functionality of the initial app Menus.
- Preloader: Used in the Preloader scene, this script initialises song data and any other data or objects that need to be initialised                before the start of the app.
- SongData: This class is used to store data on a specific song such as cues, length and composer name. Furthermore, it is used to                   transfer this song data between scenes.
- SongLoader: Class used to load song data onto from the serialized saved data on the device.
- SelectionSetup: This script initialises and controls the functionality of the SongSelection Scene.
- MenuScript: This script controls the functionality for the Menu Scene.
### -- Watson
This folder contains the Scripts folder, which contains the scripts that interact with the IBM Watson API and handles functionality for the Watson Scene. Furthermore, this is where all folders containing the unity sdk for IBM Watson are. 
#### ---- Scripts
- SpeachToText: Controls the conversion of speech from the user, into text that can be displayed on the screen.
- WatsonToneAnalyzer: Controls the analysis of the tone of the user.
- SongSelector: Controls song selection based on the tone analysis performed by Watson.
- PermissionsRationaleDialog: Controls permissions rationale
- MicrophoneTest: Controls microphone permissions
- Fader: Controls the fade in and appearance of the text and microphone in the Watson Scene.
### -- Resources
#### ---- Sounds
Within the Sounds folder, we store all the stems for the songs used in the game. Currently, there is only one song prepared in the Sounds folder. The clip names must follow the same naming system: 'Instrument' + '\_' + 'Name of song'. For example, the file name for the Cello in A Journey To Heaven is 'Cello_AJourneyToHeaven'.
