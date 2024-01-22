import express, { Express, Request, Response , Application } from 'express';
import dotenv from 'dotenv';

//For env File 
dotenv.config();

const app: Application = express();
const port = process.env.PORT || 8000;

app.get('/', async (req: Request, res: Response) => {
  res.send('Welcome to Express & TypeScript Server');

  await logTodo1();
});

app.listen(port, () => {
  console.log(`Server is Fire at http://localhost:${port}`);
});

async function logTodo1() {
  const response = await fetch("https://jsonplaceholder.typicode.com/todos/1");
  const todo = await response.json();
  console.log(todo);
}