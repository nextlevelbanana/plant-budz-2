-> the_ask

=== the_ask ===
Hey so, I was wondering, would you like to join me for an afternoon at... #mood.eh #who.you
+ [the aquarium]
    He stares at you with big eyes. The barest hint of a smile crosses his face. #mood.happy #who.fish
    ++[I'll take that as a yes!]
        -> aquarium
    ++[It doesn't HAVE to be the aquarium...]
        -> the_ask
+ [the cat cafe]
    He stares at you with big, round eyes, his mouth forming a little O. #mood.mad #who.fish
        ++[I'm not hearing a no...]
            -> cat_cafe
        ++[It doesn't HAVE to be the cat cafe...]
        -> the_ask

=== cat_cafe ===    
There's a room full of cats lounging around behind big glass windows, which they'll let you inside for an extra fee. Your date seems nervous, staring around with big eyes and jumping every time a cat makes a sudden move. You need to put him at ease.
    * [Try for conversation.]
        -> cat_converse
    * [Suggest going into the cat room.]
        -> cat_room

= cat_converse
Time to pull out one of your no-fail icebreakers! #who.you #mood.eh
    +So... do you like... cats?
    +If you got to pick one superpower, which would it be?
    +Do you think the cats think <i>we're</i> the ones in the cage?
    - He stares at you blankly for several seconds before making some soft burbling noises. #mood.eh #who.fish
        ++ [Try another topic.]
            -> cat_converse
        ++ [Suggest going into the cat room.]
            Clearly you need to liven things up, stat.
            -> cat_room
= cat_room
At your suggestion, he turns to stare at the cats in the window, which you take as a promising sign. #mood.eh

He follows you into the room. Almost as one, the cats looks up at you, tails twitching with interest. Several of them ready themselves to pounce. #mood.mad
"Aww, they're so playful!" #who.you

Your date takes one huge step back before he falls on the floor and starts thrashing around.
    + He's having a seizure!
        -> help
    + He's faking it!
        -> leave
= help
You yell for help. The barista calls 911. He doesn't stop thrashing. In fact, he thrashes himself right back out of the room.

By then the paramedics have arrived, and they waste no time whisking him off to the hospital.
"Call me!" You yell, as they close the ambulance doors.

<b> THE END </b>
-> END

= leave
You've heard of people faking illnesses to get out of bad dates, but this is ridiculous.
(And hey, was it seriously going <i>that</i> badly?)

"Ok ok, I can take a hint, <i>geez</i>," you grumble, stepping over him and out of his life.

<b>THE END</b>
-> END

=== aquarium === 
Your date is totally entranced by the aquarium. He stares around at everything, mouth hanging open in wonder. Sometimes he gets so close, he actually bumps into the glass, it's adorable! #mood.happy
-> silence

= silence
He's awfully quiet, though, and it's starting to make you nervous.
    * [Silence is golden.]
        You decide to follow his lead and stay quiet. He's having a good time, you're having a good time, who needs conversation?
        -> stingray
    * [Try for conversation.]
        -> converse

= converse
        You decide to try to get to know him better.
        ++So... come here often?
        ++So... what do you do for fun?
        ++So... what's your favorite fish?
        --He stares at you blankly for a moment before walking face first into another tank.
            +++ [Try another topic.]
                -> converse
            +++ [Try for companionable silence.]
                Ok, so he's an introvert. You decide to follow his lead and stay quiet.
                -> stingray

= stingray
Eventually, your find yourselves looking at the open pool in the stingray room. They actually let you reach in and pet them! 
    * [Demonstrate for your date.]
        "Look, you can pet them!" You stick your hand in the water.

        -> pool
    * [Wait and see what he does.]
        -> pool

= pool
Suddenly, there's a huge SPLASH!

It's your date! He's fallen into the pool!

No, he didn't fall--he sticks his head above water and grins at you. He totally jumped in on purpose.
    * [Act like you don't know him.]
        You stare at him for a second in utter disbelief. What kind of person could possibly have such disregard for the stingrays' delicate ecosystem?? 
        
        -> nope_end
    * [Yell at him to get out.]
        "What the heck are you doing? Get out of there!"

        He dives underwater and follows the stingrays around the pool, blatantly ignoring you.
        -> nope_end
    * [Join him.]
        What a free spirit! You've got to show him you accept his quirkiness. Quickly setting aside your wallet and phone, you jump in after him, laughing and splashing.   

        ** [That's when the stingrays attack.]
            -> attack
-> END
=== attack ===
You thought they were just coming to say hello, but you were wrong. Very wrong.

As the paramedics load you into the ambulance, you open your eyes enough to see you date. His eyes are wide with fear. You give him a reassuring smile.
"Call me!" You croak, just before they shut the doors.

<b> THE END </b>
-> END
=== nope_end ===
Without another word, you turn away and hurry out of the aquarium. Oof, talk about a bullet dodged. 

Still... you've had worse dates. Maybe he'll call.

<b> THE END </b>
-> END