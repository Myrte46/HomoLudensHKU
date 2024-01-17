LIST Game = TicTacToe, RockPaperScissors
LIST Replay = ReplayTrue, ReplayFalse

-> Main

=== Main ===
~ Replay = ReplayFalse
This is a world where no play is allowed. There is no court, no war, no art. You aren’t allowed to dream, you aren’t allowed to wish, you aren’t allowed to have fun.

You wake up on a regular day. Nothing is out of the ordinary. Do you go to work?

+ [Yes] >    Yes
-> work
+ [No] >    No
-> home


=== work ===
LIST beverage = coffee, tea


You go through your morning routine, you arrive to work exactly on time as usual.
-> coffeeTea
= coffeeTea
Do you take a cup of coffee or tea with you to your desk?
+ [coffee]>    Coffee
~ beverage = coffee
-> working
+ [tea]>    Tea
~ beverage = tea
-> working
= working
You arrive at your desk with your {beverage}, and start working. You work in an office that, for every 2.5 minutes that pass, you turn the clocks back 1 minute and 1 seconds.
	2.5 minutes pass. Do you turn the clock back 1 minute and 1 seconds?
    + [Yes]>    Yes
        You do this the entire day.
        ->goHome
    + [No]>    No
     -> replay
        
= goHome
At the end, do you go home?
+ [Yes]>    Yes
     -> replay
+ [No]>    No
     -> coffeeTea
     

=== replay
You lose
You played this game
Replay?
    + [Yes]
    ~ Replay = ReplayTrue
    >    Yes
    
    -> Main
    + [No]>    No
    -> END
    
=== home

You get an Email, do you open it?
+ [Yes]>    Yes
    -> invite
+ [No]>    No
    -> passTime

= invite
The Email contains an invite:
Do you want to play a game?
+ [Yes]>    Yes
    ->ChooseGame
+ [No]>    No
    ->replay
= ChooseGame
What game do you want to play?
+ [Tic Tac Toe]>    Tic Tac Toe
~ Game = TicTacToe
-> replay
+ [Rock Paper Scissors]>    Rock Paper Scissor
~ Game = RockPaperScissors
-> replay

= passTime
LIST Doing = Something, Nothing
What do you do to pass the time?
+[Something]>    Something
~ Doing = Something
-> ReadEmail
+[Nothing]>    Nothing
~ Doing = Nothing
-> ReadEmail

= ReadEmail
You do {Doing}. The email is still there, do you open it?
+ [Yes]>    Yes
-> invite
+ [No]>    No
-> replay

