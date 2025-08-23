import {useEffect, useRef, useState} from "react";
import {Text} from "@consta/uikit/Text";
import {User as ConstaUser} from "@consta/uikit/User";
import {useAuth} from "../../../context/AuthContext";
import {formatFio} from "../../../utils/string-utils.ts";
import {RoleCodes, Roles} from "../../../types/roles.ts";

import "./header.css";

function getDisplayName(profile: any | null): string {
    if (!profile)
        return "Пользователь";
    if(profile.roles.length === 1 && profile.roles[0] == RoleCodes.ADMIN)
        return "Администратор";
    const first = profile.firstName || profile.given_name;
    const last = profile.lastName || profile.family_name;
    const patronymic = profile.patronymic || profile.patronymic;
    if (first || last)
        return formatFio(`${last} ${first} ${patronymic}`);
    return profile.username || profile.email || "Пользователь";
}

function getDisplayRole(profile: any | null): string {
    const roles = profile?.roles || [];
    return roles.length > 0 ? Roles[roles[0] as keyof typeof Roles] : "";
}

export default function Header() {
    const menuItems = [{label: "Стенды", id: "stands"}];

    const [activeItem, setActiveItem] = useState<string>(menuItems[0].id);
    const [isMenuOpen, setIsMenuOpen] = useState(false);
    const menuRef = useRef<HTMLDivElement>(null);

    const {keycloak, authenticated, user, logout} = useAuth();

    useEffect(() => {
        function handleClickOutside(e: MouseEvent) {
            if (menuRef.current && !menuRef.current.contains(e.target as Node)) {
                setIsMenuOpen(false);
            }
        }

        document.addEventListener("mousedown", handleClickOutside);
        return () => document.removeEventListener("mousedown", handleClickOutside);
    }, []);



    return (
        <div className="header">
            <div className="logo">
                <Text size="l" weight="bold">
                    PREDITRIX
                </Text>
            </div>

            {authenticated && (
                <div className="navigationMenu">
                    {menuItems.map((item) => (
                        <div
                            key={item.id}
                            className={`menuItem ${activeItem === item.id ? "active" : ""}`}
                            onClick={() => setActiveItem(item.id)}
                        >
                            <Text as="span" size="s">
                                {item.label}
                            </Text>
                        </div>
                    ))}
                </div>
            )}

            {authenticated && (
                <div
                    className="user"
                    ref={menuRef}
                    style={{position: "relative", cursor: "pointer"}}
                    onClick={() => setIsMenuOpen((prev) => !prev)}
                >
                    <ConstaUser
                        name={getDisplayName(user)}
                        info={getDisplayRole(user)}
                        width="full"
                        size="l"
                        withArrow
                    />

                    {isMenuOpen && (
                        <div className="userDropdown">
                            <div
                                className="dropdownItem"
                                onClick={(e) => {
                                    e.stopPropagation();
                                    setIsMenuOpen(false);
                                    logout();
                                }}
                            >
                                Выйти
                            </div>
                        </div>
                    )}
                </div>
            )}
        </div>
    );
}
