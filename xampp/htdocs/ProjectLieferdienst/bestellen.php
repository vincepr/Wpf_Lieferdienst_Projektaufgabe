<?php
    
    // incoming Get-Request form C#-Application:
    $json = file_get_contents('php://input');
    $daten = json_decode($json, true);
    $eid = $daten["eid"];
    $anzahl = $daten["anzahl"];


    // Connect to the DB-Server
    $db = new mysqli("localhost", "ronny", "1234", "lieferdienst");

    // define the SQL reuqest - we use the ? syntax to get request the ypes so we can validate them:
    $request = "INSERT INTO bestellung (datum, eid, anzahl) VALUES (now(), ?, ?)";
    $insert = $db->prepare($request);
    $insert->bind_param("ii", $eid, $anzahl);
    $insert->execute();
    // We return the Number of affected Rows to validate for success in our C# App. Will return nr-of affected rows. We expect "1"
    print mysqli_affected_rows($db);    
    $db->close();

    
?>

