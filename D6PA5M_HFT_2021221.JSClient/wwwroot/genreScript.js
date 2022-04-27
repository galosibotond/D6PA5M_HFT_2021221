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

    connection.on("GenreUpdated", (user, message) => {
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
        `<button name="deleteButton" type="button" onclick="remove(${t.id})">Delete</button>` +
        `<button name="updateButton" type="button" onclick="update(${t.id})">Update</button>` +
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

function update(id) {
    let genreIndex = genres.findIndex(genre => genre.id == id);
    let oldGenre = genres[genreIndex];
    let newName = prompt("Updated genre name:", oldGenre.name);
    if (newName != null && newName != "") {
        fetch('http://localhost:36957/genre', {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json', },
            body: JSON.stringify({
                id: oldGenre.id,
                name: newName
            })
        })
            .then(response => response)
            .then(data => {
                console.log('Success:', data);
                getdata();
            })
            .catch((error) => { console.error('Error:', error); });
    }
    else {
        alert("You did not provided a new genre name, please try again!")
    }

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