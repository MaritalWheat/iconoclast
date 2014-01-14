iconoclast
==========

TODO/Bugs
-Right now each player submits their character coords relative to their own board. Either the host manager or the srver needs to convert the clients coords to the opposite side of the board
-The Server needs to push the results of the move back out to the Network Manager.

Networking
There is 2 Network Managers, one in the client and one in the host. These are the only things that need to make network calls. The host will also have the "Server" class. The server will process the turns and then send the results to the network manager who will distribute the data appropriately.
