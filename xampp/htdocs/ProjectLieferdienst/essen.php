<?php
    
    // Connection to the DB-Server
    $db = new mysqli("localhost", "ronny", "1234", "lieferdienst");

    // define the SQL reuqest
    $request = "SELECT eid, bezeichnung, preis, info, bild FROM essen";

    // send request to DB-Server -> get pointer pointing at start of table
    $table = $db->query($request);

    // fetch_assoc() - fetches a result row as an associative array.
    $line = $table->fetch_assoc();
    $data = array(); 

    // keep reading lines
    while ($line == true){
        //print "$line[eid] <br/> $line[bezeichnung] <br/> $line[preis] <br/> <br/>";
        array_push($data, $line);
        $line = $table->fetch_assoc();
    }
    $text = json_encode($data, JSON_UNESCAPED_SLASHES);
    //print_r($data);
    print $text;

    // close connection when done:
    $db->close();
    
?>

