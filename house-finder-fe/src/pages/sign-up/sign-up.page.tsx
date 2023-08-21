import {useEffect, useState} from "react";
import "./sign-up.scss"
import {FormControl} from "@mui/material";

export const SignUpPage = () => {
    const [animateCircles, setAnimateCircles] = useState(false);
    const [animateForm, setAnimateForm] = useState(false);
    useEffect(() => {
        handleAnimationStart();
    }, []);
    const handleAnimationStart = () => {
        setAnimateCircles(true);
        setAnimateForm(true);
    };
    return (
        <div className={`container-sign-up ${animateCircles ? 'animate-circles-up' : ''}`}>
            <div className={`background-up ${animateCircles ? 'animate-circles-up' : ''}`}>
                <div className="shape-up"></div>
                <div className="shape-up"></div>
            </div>
            <form className={`sign-up-form ${animateForm ? 'animate-form' : ''}`}>
                <h3>Sign Up</h3>
                <FormControl>
                    <label htmlFor="username">Email</label>
                    <input type="text" placeholder="Email" id="username"/>
                </FormControl>
                <FormControl>
                    <label htmlFor="phone">Phone</label>
                    <input type="text" placeholder="Phone" id="phone"/>
                </FormControl>
                <div className={'row'}>
                    <div className={'input-col'}>
                        <FormControl>
                            <label htmlFor="first-name">First Name</label>
                            <input type="text" placeholder="First Name" id="first-name"/>
                        </FormControl>
                    </div>
                    <div className={'input-col'}>
                        <FormControl>
                            <label htmlFor="last-name">Last Name</label>
                            <input type="text" placeholder="Last Name" id="last-name"/>
                        </FormControl>
                    </div>
                </div>
                <FormControl>
                    <label htmlFor="password">Password</label>
                    <input type="password" placeholder="Password" id="password"/>
                </FormControl>
                <FormControl>
                    <label htmlFor="password-confirm">Confirm Password</label>
                    <input type="password" placeholder="Confirm Password" id="password-confirm"/>
                </FormControl>
                <button className={'btn'}>Sign Up</button>
                <p className={'terms'}>By submitting, I accept House finder <a href={''}>terms of use.</a></p>
            </form>
        </div>
    );
};
