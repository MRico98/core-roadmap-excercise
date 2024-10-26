import React from "react";
import NavBar from "../organisms/NavBar";
import NewUserForm from "../organisms/NewUserForm";

const NewUserTemplate: React.FC = () => {
    return (
        <div>
            <h1>Welcome to TeamSpace!</h1>
            <NavBar isLoggedIn={false} onLogin={function (): void {
                throw new Error("Function not implemented.");
            } } onLogout={function (): void {
                throw new Error("Function not implemented.");
            } } username={""} />
            <h2>Create a new user</h2>
            <NewUserForm />
        </div>
    );
}

export default NewUserTemplate;