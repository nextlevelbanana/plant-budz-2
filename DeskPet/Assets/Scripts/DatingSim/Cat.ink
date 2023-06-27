-> the_ask

=== the_ask ===
Hey so, I was wondering, would you like to join me for an afternoon at... #mood.eh 
+ [the aquarium]
    Her eyes flash with a predatory glee. #mood.happy
    ++[I'll take that as a yes!]
        -> aquarium
    ++[It doesn't HAVE to be the aquarium...]
        -> the_ask
+ [the cat cafe]
    She holds your gaze for five full seconds and then licks the back of her hand. #mood.eh 
        ++[I guess this is how the kids flirt these days!]
            -> cat_cafe
        ++[It doesn't HAVE to be the cat cafe...]
            -> the_ask

=== cat_cafe ===    
There's a room full of cats lounging around behind big glass windows, which they'll let you inside for an extra fee. You think it's cute, but your date seems unimpressed. She gives the cats the barest of glances before sitting down at a table.
    + [Try for conversation.]
        -> cat_converse
    + [Suggest going into the cat room.]
        Time to liven things up. You suggest petting the cats.
        -> cat_room

= cat_converse
Time to pull out one of your no-fail icebreakers!  #mood.eh
    +So... do you like... cats?
        Your date lifts her arms over her head in an enormous stretch, then lays her head on the table.
    +If you got to pick one superpower, which would it be?
        Your date ignores you, choosing instead to bat around a straw wrapper someone left on the table.
    +Do you think the cats think <i>we're</i> the ones in the cage?
        Your date notices a fly buzzing around and sits straight up, tracking its motions intently.
    - Oh no, you're losing her! #mood.eh 
        ++ [Try another topic.]
            -> cat_converse
        ++ [Suggest going into the cat room.]
            Time to liven things up. You suggest petting the cats.
            -> cat_room
= cat_room
Your date makes a noncommittal noise and sniffs cautiously at her latte. Yeah, you definitely need a change of pace. #mood.eh

Once she enters the room, she comes to life. ...but she's less entranced by the cats than their toys. She practically bounds to the far side of the room and grabs a sparkly felt ball, batting it around. #mood.happy
    + [Play along.]
        -> play
    + [Pretend not to know her.]
        -> leave
= leave
If she didn't want to go out with you, she should have just said no. This is humiliating. #mood.happy

"Oh look! An emergency call from my job. Let me just step outside and take this," you say. Not that you needed a ruse; she's so enthralled by the sparkly ball she doesn't even notice you leaving.

<b>THE END</b>
-> END

= play
She's so quirky and fun! You pick up one of those fishing pole feather things and wave it in front of her face. She looks up abruptly, gaze locked on the dancing feather. #mood.happy
    * [That's when they ask you to leave.]
        -> escorted

= escorted
The owner bangs into the room and says you two need to go, you're making the barista uncomfortable. Your date narrows her eyes and growls, but you assure the lady you don't want any trouble, and allow yourselves to be escorted to the parking lot. #mood.mad

You laugh it off. "This isn't exactly how I saw this date going, but I had a lot of fun! Maybe we could do it again sometime?" #mood.eh

She responds with a long, slow blink.
<b>THE END</b>
-> END

=== aquarium === 
Wow, your date really must love aquariums. She's bounding from room to room, staring enthralled at the fish until they swim out of view... #mood.happy

This is going great! 

...At least, until she starts slapping at the glass.
-> slap

= slap
She's slapping at the fish through the glass, no doubt scaring the bejeezus out of them.
    + [Distract her with conversation.]
        -> converse
    + [Ask her to stop.]
        "Hey, come on, cut it out," you plead. "That's bad for the fish." 

        She growls at you and swats at a passing sturgeon. A woman who knows what she wants!
        -> slap
    + [Follow her lead.]
        -> security

= converse
        You try to distract her with conversation.
        +So... come here often?
        +So... what do you do for fun?
        +So... what's your favorite cat?
        -Instead of responding, your date leaps across the room to stare down another fish.
            ++ [Try another topic.]
                -> converse
            ++ [Try a different tactic.]
                -> slap

= security
You slap the glass alongside her, trying to get a rhythm going. She has terrible timing, but you won't hold that against her. There now, aren't we having fun?
    + [That's when security shows up.]
        A very annoyed guard tells you to knock it off. Your date...

        ...your date responds by hissing at him.
            ++ [Security guards generally don't like being hissed at.]
                "Okay, you're done here," the guard says, stepping toward her. She swipes at him with her long nails.
                +++ [Fight! Fight! Fight!]
                    -> fight
                +++ [Pretend not to know her.]
                    -> leave_aq
=leave_aq
While they're both distracted, you slip away and hurry out of the aquarium. Oof, talk about a bullet dodged.

Still... you've had worse dates. Maybe she'll call.

<b> THE END </b>
-> END

=fight
You rush to defend your date's honor with all the moves you remember from your middle school karate lessons.

The guard calls for backup. The next thing you know, you and your date are being frog-marched to the parking lot.

"At least it's, you know, thematically appropriate," you joke. "You know... frogs... aquariums..." Your date doesn't dignify that with an answer.

... yeah, you're not getting a second date.
<b>THE END</b>
-> END
