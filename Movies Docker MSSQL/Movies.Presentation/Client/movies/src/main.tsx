import ReactDOM from 'react-dom/client'
import 'semantic-ui-css/semantic.min.css';
import './App.css'
import {router} from "./router/Routes.tsx";
import { RouterProvider } from 'react-router-dom';

ReactDOM.createRoot(document.getElementById('root')!).render(
      <RouterProvider router={router}/>
)
