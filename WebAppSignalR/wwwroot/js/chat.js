

"use strict";

//mengarahkan signal r request ke url wwwroot/chatHub 
//sesuai yang disetting di startup.cs app.UseSignalR(config => { config.MapHub<ChatHub>("/chatHub"); });
var connection = new signalR.HubConnectionBuilder().withUrl("/chatHub").build();

//Disable send button until connection is established
document.getElementById("sendButton").disabled = true;

//code ini utk menerima jika ada user yang mengirim pesan
connection.on("ReceiveMessage", function (user, message) {
    var msg = message.replace(/&/g, "&amp;").replace(/</g, "&lt;").replace(/>/g, "&gt;");
    var encodedMsg = user + " says " + msg;
    var li = document.createElement("li");
    li.textContent = encodedMsg;
    document.getElementById("messagesList").appendChild(li);
});

//debugger

document.getElementById('sendButton2').addEventListener('click', function () {

    connection.invoke("GetAllConnectedUser").catch(function (err) {
        return console.error(err.toString());
    });
});

connection.on("ReceiveConnectedUsers", function (test) {

    //alert('alamak');
    alert(test);
    //alert(connection.);

});

//code ini utk mengecek jika client terkoneksi dengan server, jika terkoneksi tombol send di-enable (disabled false)
connection.start().then(function () {
    //jika tidak terjadi any error maka button siap dipencet
    document.getElementById("sendButton").disabled = false;    
    //ketika connection start, butuh waktu sampai status nya connected, agar invoke sukses, panggil dengan .then
    //utk memastikan invoke di-run setelah connection start perfectly
    connection.invoke("GetAllConnectedUser").catch(err => console.error(err.toString()));
}).catch(function (err) {
    alert(err);
    return console.error(err.toString());
});



//code ini utk mengirim pesan, secara broadcast, user lain diberi signal utk menerima
document.getElementById("sendButton").addEventListener("click", function (event) {
        var user = document.getElementById("userInput").value;
        var message = document.getElementById("messageInput").value;
        var userId = document.getElementById("userId").value;
        connection.invoke("SendMessage", user, message, userId).catch(function (err) {
            return console.error(err.toString());
        });
        event.preventDefault();
    }
);


