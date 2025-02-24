import React from "react";
import Button from "../atoms/Button";

interface NavBarProps {
    isLoggedIn: boolean;
    onLogin: () => void;
    onLogout: () => void;
    username: string;
}

const NavBar: React.FC<NavBarProps> = ({ isLoggedIn, onLogin, onLogout, username }) => {
    return (
        <nav>
            <ul>
                <li>
                    <Button label="Login" onClick={onLogin} disabled={isLoggedIn} />
                </li>
                <li>
                    <Button label="Logout" onClick={onLogout} disabled={!isLoggedIn} />
                </li>
                <li>
                    {username}
                </li>
            </ul>
        </nav>
    );
};

export default NavBar;