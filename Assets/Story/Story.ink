
//2$BG01$2
//0$null$Hello There!
//0$null$General Kenobi!
//1$null$Carmela$2
//0$Carmela$Carmela$Io sono Carmela
//1$null$John$2
//0$John$John$Io sono John
//4$John$4$1
//1$Karen$Carmela$2
//0$Karen$Karen$Io sono Karen
//4$Karen$1$1
//0$Karen$Karen$E sono incazzata

2$BG01$2
1$Julia$null$2
0$Julia$Julia$Hello There!
0$Julia$Julia$This demo will walk you through all you need to know about this asset and you're going to create visul novels in no time!

->Chapter_1

== Chapter_1 ==

0$Julia$Julia$Tell me what you want to know!

+   General Setup

    0$Julia$Julia$To create a working environment, you just need to add to your scene a VisualNovelManager.
    
    0$Julia$Julia$You can personalize all the other components of this demo, as long as you keep all the components and the reference to the other components.
    
    0$Julia$Julia$Just make sure to initialize the VisualNovelManager, like the TutorialManager in this scene do.

    ->Chapter_1

+   Actors

    0$Julia$Julia$I'm an actor, identified by a script like the one you see in the image on the side.
    
    0$Julia$Julia$I'll show you how to create an actor, just follow me in this few easy steps:
    
    0$Julia$Julia$1. Import in Unity all the expressions that the actor need. 
    
    0$Julia$Julia$Keep in mind that all actors should have sprites with the same height based on the resolution you're working on.
    
    0$Julia$Julia$For example, if you're working in 1920x1080, all actor's sprites should be 1080 in height.
    
    0$Julia$Julia$2. Create a new object with a structure like the one in the image.
    
    0$Julia$Julia$Add the character's emotions to the Actor script and remember that the emotions will have an id bound to their place in the array.
    
    0$Julia$Julia$Now save this object as a prefab.
    
    0$Julia$Julia$3. In the Actor script, remember to set the name field as the name that you want the actor to have.
    
    0$Julia$Julia$4. Add the prefab to the actor list in the Character Manager in the VisualNovelManager object
    
    0$Julia$Julia$And you're good to go, that's all you need to do.

    ->Chapter_1

+   Background 

    0$Julia$Julia$To add new backgrounds to the scene, just import the image in unity and then add them to the list in the VisualNovelLayersArea.
    
    0$Julia$Julia$Just make sure that they all have a different name.

    ->Chapter_1

+   Ink

    0$Julia$Julia$Thanks to Ink, after setting up actors and background, you just have to write down everything that you need Unity to do.
    
    0$Julia$Julia$To write, you have to use Inkle, a very useful editor from ink's creators. But you need to format your text in a peculiar way for this asset to work.
    
    0$Julia$Julia$Obviously you can use all the tools ink offer, like choices or variables.
    
    ->Chapter_2
    
    == Chapter_2 ==
    
    0$Julia$Julia$In This Version, we have 5 different commands implemented, identified by an id.
    
    ++   Write Dialogue
    
        0$Julia$Julia$Use this command to write dialogues on screen.
        
        0$Julia$Julia$You can use this to let a narrator talks or one of your actor in scene.
        
        0$Julia$Julia$The first name in the command is the name of the actor that needs to talk. If this is null, the the narrator will talk.
        
        0$Julia$Julia$If you have selected an actor, the second name will specify how the actor should be called in the text area.
    
        ->Chapter_2
    
    ++   Place Actor
    
        0$Julia$Julia$Use this command to place actors on the scene.
        
        0$Julia$Julia$With this, you can call upon two actors at the same time. The first name will be the actor on the left and the second the name of the one on the right.
        
        0$Julia$Julia$The last number indicates how much time the animation will take.
        
        0$Julia$Julia$Keep in mind that if you want to change actor in a spot, the active actor will be removed and than the new actor will appear, resulting in twice the wait time.
        
        0$Julia$Julia$Writing null in one of the name spots will keep the space free or remove the actor present in that spot.
    
        ->Chapter_2
    
    ++   Change Background
    
        0$Julia$Julia$Use this command to change Background.
        
        0$Julia$Julia$The first name indicates the background that will be active. You can also indicate null to remove the background.
        
        0$Julia$Julia$The last number indicates how much time the animation will take.
        
        0$Julia$Julia$Keep in mind that if you want to change background, there will be a fade out and than a fade in resulting in twice the animation time.
    
        ->Chapter_2
    
    ++   Wait
    
        0$Julia$Julia$Use this command to wait for a certain number of seconds
    
        ->Chapter_2
    
    ++   Change Emotion
    
        0$Julia$Julia$Use this command to change the emotion of a character.
        
        0$Julia$Julia$This command will execute even if the actor is not active in scene.
        
        0$Julia$Julia$Use the first parameter to specify the actor that needs to change emotion,
        
        0$Julia$Julia$the second to specify the id of the wanted emotion
        
        0$Julia$Julia$and the third to indicate how much time the animation will take
    
        ->Chapter_2
    
    ++   Done
    
        ->Chapter_1













