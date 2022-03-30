let albums = [];
let artists = [];
let recordCompanies = [];
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

    connection.on("AlbumCreated", (user, message) => {
        getdata();
    });

    connection.on("AlbumDeleted", (user, message) => {
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
    await fetch('http://localhost:36957/album')
        .then(x => x.json())
        .then(y => {
            albums = y;
            display();
        });

    await fetch('http://localhost:36957/artist')
        .then(x => x.json())
        .then(y => {
            artists = y;
        });

    await fetch('http://localhost:36957/recordCompany')
        .then(x => x.json())
        .then(y => {
            recordCompanies = y;
        });

    await fetch('http://localhost:36957/genre')
        .then(x => x.json())
        .then(y => {
            genres = y;
        });
}

function display() {
    document.getElementById('tableArea').innerHTML = "";
    albums.forEach(t => {
        document.getElementById('tableArea').innerHTML +=
            "<tr><td>" + t.id + "</td><td>" +
            t.title + "</td><td>" +
            t.artist.name + "</td><td>" +
            new Date(t.releaseDate).toLocaleDateString() + "</td><td>" +
            t.recordCompany.name + "</td><td>" +
            t.price + "</td><td>" +
            t.stock + "</td><td>" + 

            `<button type="button" onclick="remove(${t.id})">Delete</button>`
            + "</td></tr>";
    });
}

function remove(id) {
    fetch('http://localhost:36957/album/' + id, {
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
    let title = document.getElementById('albumTitle').value;
    let stock = document.getElementById('albumStock').value;
    let price = document.getElementById('albumPrice').value;
    let releaseDate = document.getElementById('albumReleaseDate').value;
    let artistId;
    let artistIndex = getEntityIndex(artists, 'albumArtistName');

    if (artistIndex == -1) {
        alert('Artist does not exist. Please create the artist before creating the album!');
        return;
    }

    let recordCompanyId;
    let recordCompanyIndex = getEntityIndex(recordCompanies, 'albumRecordCompany');

    if (recordCompanyIndex == -1) {
        alert('Record Company does not exist. Please create the record company before creating the album!');
        return;
    }
    recordCompanyId = recordCompanies[recordCompanyIndex].id;

    fetch('http://localhost:36957/album', {
        method: 'POST',
        headers: { 'Content-Type': 'application/json', },
        body: JSON.stringify(
            {
                title: title,
                artistId: artistId,
                recordCompanyId: recordCompanyId,
                stock: stock,
                price: price,
                releaseDate: releaseDate
            })
    })
        .then(response => response)
        .then(data => {
            console.log('Success:', data);
            (async () => {
                await (() => getdata())
            })();
        })
        .catch((error) => { console.error('Error:', error); });

}

function getEntityIndex(sourceArray, elementName) {
    let entityIndex = -1;

    entityIndex = sourceArray.findIndex(entity => entity.name == document.getElementById(elementName).value);

    return entityIndex;
}