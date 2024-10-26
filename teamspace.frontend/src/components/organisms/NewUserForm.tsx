import React from 'react';
import Button from '../atoms/Button';
import FormField from '../molecules/FormField';

const NewUserForm: React.FC = () => {
    const [firstName, setFirstName] = React.useState("Manuel");
    const [lastName, setLastName] = React.useState("");
    const [email, setEmail] = React.useState("");
    const [password, setPassword] = React.useState("");

    const createUser = () => {
        console.log("Creating user...");
        console.log("First Name: ", firstName);
        console.log("Last Name: ", lastName);
        console.log("Email: ", email);
    };

    return (
        <form>
            <FormField label="First Name" type="text" value={"Manuel"} placeholder="Enter your first name" onChange={setFirstName} />
            <FormField label="Last Name" type="text" value={lastName} placeholder="Enter your last name" onChange={setLastName} />
            <FormField label="Email" type="email" value={email} placeholder="Enter your email" onChange={setEmail} />
            <FormField label="Password" type="password" value={password} placeholder="Enter your password" onChange={setPassword} />
            <Button label="Create User" onClick={createUser} />
        </form>
    );
}

export default NewUserForm;