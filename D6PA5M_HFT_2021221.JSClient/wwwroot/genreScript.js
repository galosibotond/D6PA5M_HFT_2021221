let genres = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:36957/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("GenreCreated", (user, message) => {
        getdata();
    });

    connection.on("GenreDeleted", (user, message) => {
        getdata();
    });

    connection.onclose(async () => {
        await start();
    });
    start();


}

async function start() {
    try {
        await connection.start();
        console.log("SignalR Connected.");
    } catch (err) {
        console.log(err);
        setTimeout(start, 5000);
    }
};

async function getdata() {
    await fetch('http://localhost:36957/genre')
        .then(x => x.json())
        .then(y => {
            genres = y;
            display();
        });
}

function display() {
    document.getElementById('tableArea').innerHTML = "";
    genres.forEach(t => {
        document.getElementById('tableArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" +
            t.name + "</td><td>" +
            `<button type="button" onclick="remove(${t.id})">Delete</button>` +
            "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:36957/genre/' + id, {
        method: 'DELETE',
        headers: { 'Content-Type': 'application/json', },
        body: null
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function create() {
    let name = document.getElementById('genreName').value;

    fetch('http://localhost:36957/genre', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}