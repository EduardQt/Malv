import 'bootstrap/dist/css/bootstrap.min.css';
import React from 'react';
import ReactDOM from 'react-dom/client';
import './index.scss';
import reportWebVitals from './reportWebVitals';
import {createBrowserRouter, Navigate, RouterProvider} from "react-router-dom";
import {LoginComponent} from "./app/auth/LoginComponent";
import {RegisterComponent} from "./app/auth/RegisterComponent";
import {SignOutComponent} from "./app/auth/SignOutComponent";
import {AuthComponent} from "./app/auth/AuthComponent";
import {AdComponent} from "./app/ads/AdComponent";
import {AdCreateComponent} from "./app/ads/create/AdCreateComponent";
import {AdViewComponent} from "./app/ads/view/AdViewComponent";
import {LandingPageComponent} from "./app/home/LandingPageComponent";
import {HomeComponent} from "./app/home/HomeComponent";
import {AdsComponent} from "./app/home/AdsComponent";
import {ChatComponent} from "./app/chat/ChatComponent";
import {MyChatsComponent} from "./app/chat/my/MyChatsComponent";
import {VerifyMailComponent} from "./app/auth/VerifyMailComponent";
import {AdWatchesComponent} from "./app/ads/watches/AdWatchesComponent";

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
);

const router = createBrowserRouter([
    {
        path: "/",
        element: <Navigate to={'/Home/Landing'} />,
    },
    {
        path: "Home",
        element: <HomeComponent />,
        children: [
            {path: "Landing", element: <LandingPageComponent />},
            {path: "Ads", element: <AdsComponent />},
        ]
    },
    {
        path: "Auth",
        element: <AuthComponent />,
        children: [
            {path: "Login", element: <LoginComponent />},
            {path: "Register", element: <RegisterComponent />},
            {path: "SignOut", element: <SignOutComponent />},
            {path: "VerifyMail", element: <VerifyMailComponent />}
        ]
    },
    {
        path: "Ad",
        element: <AdComponent />,
        children: [
            {path: "Create", element: <AdCreateComponent />},
            {path: "View", element: <AdViewComponent />},
            {path: "Watches", element: <AdWatchesComponent />},
        ]
    },
    {
        path: "Chat",
        element: <ChatComponent />,
        children: [
            {path: "My", element: <MyChatsComponent />}
        ]
    }
]);


root.render(
    <div className={'bg-light'}>
        <RouterProvider router={router} />
    </div>
);

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals();
