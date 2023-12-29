LIST Game = TicTacToe, RockPaperScissors

-> Main

=== Main ===
This is a world where no play is allowed. There is no court, no war, no art. You aren’t allowed to dream, you aren’t allowed to wish, you aren’t allowed to have fun.

You wake up on a regular day. Nothing is out of the ordinary. Do you go to work?
+ [Yes]
-> work
+ [No]
-> home


=== work ===
LIST beverage = coffee, tea


You go through your morning routine, you arrive to work exactly on time as usual.
-> coffeeTea
= coffeeTea
Do you take a cup of coffee or tea with you to your desk?
+ [coffee]
~ beverage = coffee
-> working
+ [tea]
~ beverage = tea
-> working
= working
You arrive at your desk with your {beverage}, and start working. You work in an office that, for every 2.5 minutes that pass, you turn the clocks back 1 minute and 1 seconds.
	2.5 minutes pass. Do you turn the clock back 1 minute and 1 seconds?
    + [Yes]
        You do this the entire day.
        ->goHome
    + [No]
     -> replay
        
= goHome
At the end, do you go home?
+ [Yes]
     -> replay
+ [No]
     -> coffeeTea
     

=== replay
You lose
You played this game
Replay?
    + [Yes]
    -> Main
    + [No]
    -> END
    
=== home

You get an Email, do you open it?
+ [Yes]
    -> invite
+ [No]
    -> passTime

= invite
The Email contains an invite:
Do you want to play a game?
+ [Yes]
    ->ChooseGame
+ [No]
    ->replay
= ChooseGame
What game do you want to play?
+ Tic Tac Toe
~ Game = TicTacToe
-> replay
+ Rock Paper Scissors
~ Game = RockPaperScissors
-> replay

= passTime
LIST Doing = Something, Nothing
What do you do to pass the time?
+[Something]
~ Doing = Something
-> ReadEmail
+[Nothing]
~ Doing = Nothing
-> ReadEmail

= ReadEmail
You do {Doing}. The email is still there, do you open it?
+ [Yes]
-> invite
+ [No]
-> replay

