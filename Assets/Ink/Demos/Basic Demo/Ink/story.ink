VAR know_about_wager = false

{ know_about_wager:
	"But surely you are not serious?" I demanded.
	->london
	
- else:
->astonished
 "But there must be a reason for this trip," I observed.
}


=== london ===
Monsieur Phileas Fogg returned home early from the Reform Club, and in a new-fangled steam-carriage, besides!  
"Passepartout," said he. "We are going around the world!"
-> ending


=== astonished ===
"You are in jest!" I told him in dignified affront. "You make mock of me, Monsieur."
"I am quite serious."
    -> nod


=== nod ===
I nodded curtly, not believing a word of it.
-> ending


=== ending ===
"We shall circumnavigate the globe within eighty days." He was quite calm as he proposed this wild scheme. "We leave for Paris on the 8:25. In an hour."
->DONE


-> END
=== function know_of_wager(x) ===
    hello world