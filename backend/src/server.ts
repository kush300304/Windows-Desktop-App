import express from 'express';
import bodyParser from 'body-parser';
import fs from 'fs';

const app = express();
const port = 3000;
const dbFilePath = 'db.json';

app.use(bodyParser.json());

// Endpoint to check server status
app.get('/ping', (req, res) => {
    res.send(true);
});

// Endpoint to submit form data
app.post('/submit', (req, res) => {
    const { name, email, phone, github_link, stopwatch_time } = req.body;
    if (!name || !email || !phone || !github_link || !stopwatch_time) {
        return res.status(400).send('All fields are required.');
    }
    const submission = { name, email, phone, github_link, stopwatch_time };
    let db = [];
    if (fs.existsSync(dbFilePath)) {
        const data = fs.readFileSync(dbFilePath, 'utf8');
        db = JSON.parse(data);
    }
    db.push(submission);
    fs.writeFileSync(dbFilePath, JSON.stringify(db, null, 2));
    res.send('Submission saved.');
});

// Endpoint to read submissions
app.get('/read', (req, res) => {
    if (fs.existsSync(dbFilePath)) {
        const data = fs.readFileSync(dbFilePath, 'utf8');
        const db = JSON.parse(data);
        console.log(db)
        res.send(db);
    } else {
        res.status(404).send('No submissions found.');
    }
});

app.listen(port, () => {
    console.log(`Server is running on http://localhost:${port}`);
});
