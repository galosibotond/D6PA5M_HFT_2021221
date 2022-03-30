let artists = [];
let genres = [];
let checkboxNames = [];
let connection = null;
getdata();
setupSignalR();

function setupSignalR() {
    connection = new signalR.HubConnectionBuilder()
        .withUrl("http://localhost:36957/hub")
        .configureLogging(signalR.LogLevel.Information)
        .build();

    connection.on("ArtistCreated", (user, message) => {
        getdata();
    });

    connection.on("ArtistDeleted", (user, message) => {
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
    await fetch('http://localhost:36957/artist')
        .then(x => x.json())
        .then(y => {
            artists = y;
            display();
        });

    await fetch('http://localhost:36957/genre')
        .then(x => x.json())
        .then(y => {
            genres = y;
        });

    if (checkboxNames.length == 0) {
        createGenreCheckboxes();
    }
}

function display() {
    document.getElementById('tableArea').innerHTML = "";
    artists.forEach(t => {
        document.getElementById('tableArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" +
            t.name + "</td><td>" +
            t.genre.name + "</td><td>" +
            t.country + "</td><td>" +
            new Date(t.foundationDate).toLocaleDateString() + "</td><td>" +

            `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:36957/artist/' + id, {
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
    let name = document.getElementById('artistName').value;
    let foundationDate = document.getElementById('artistFoundationDate').value;
    let country = document.getElementById('artistCountry').value;
    let genreId;
    let genreIndex = -1;

    for (var i = 0; i < checkboxNames.length; i++) {
        if (document.getElementById(checkboxNames[i]).checked) {
            genreIndex = genres.findIndex(genre => genre.name == document.getElementById('genreLabel' + i.toString()).textContent);   
        }
    }

    if (genreIndex == -1) {
        alert('No genre selected, please select or create a genre you want to use!');
        return;
    }
    else {
        genreId = genres[genreIndex].id;
    }

    fetch('http://localhost:36957/artist', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                name: name,
                genreId: genreId,
                country: country,
                foundationDate: foundationDate,
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            getdata();
        })
        .catch((error) => { console.error('Error:', error); });

}

function createGenreCheckboxes() {
    document.getElementById('genreDiv').innerHTML +=
        '<label>Genre</label>';

    for (var i = 0; i < genres.length; i++) {
        document.getElementById('genreDiv').innerHTML +=
            '<label id="genreLabel' + i.toString() + '"><input type="checkbox" id="genreCheckbox' + i.toString() + '" onclick="selectOnlyThis(id)"/>' +
            genres[i].name + '</label>';

        checkboxNames.push('genreCheckbox' + i.toString());
    }
}

function selectOnlyThis(id) {
    for (var i = 0; i < checkboxNames.length; i++)
    {
        document.getElementById(checkboxNames[i]).checked = false;
    }

    document.getElementById(id).checked = true;
}