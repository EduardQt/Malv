import {useState} from "react";
import {Button, Form, FormFeedback, FormGroup, FormText, Input, Label} from "reactstrap";
import {ServiceContainer} from "../../services/ServiceContainer";

export function RegisterComponent() {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [confirmPassword, setConfirmPassword] = useState<string>('');
    const [firstName, setFirstName] = useState<string>('');
    const [lastName, setLastName] = useState<string>('');

    const onSubmit = (event: any) => {
        event.preventDefault();
        ServiceContainer.authService.register({
            email: email,
            password: password,
            confirmPassword: confirmPassword,
            firstName: firstName,
            lastName: lastName
        }).then(() => {

        }).catch(error => {
            console.log('Error');
            console.log(error);
        })
    }

    return (
        <div className="ms-auto me-auto col-lg-6 col-md-8 col-sm-12">
            <Form>
                <FormGroup>
                    <Label for="registerEmail">
                        Email
                    </Label>
                    <Input id={'registerEmail'} type={'email'} value={email}
                           onChange={(event) => setEmail(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="registerPassword">
                        Password
                    </Label>
                    <Input id={'registerPassword'} type={'password'} value={password}
                           onChange={(event) => setPassword(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="registerConfirmPassword">
                        Confirm Password
                    </Label>
                    <Input id={'registerConfirmPassword'} type={'password'} value={confirmPassword}
                           onChange={(event) => setConfirmPassword(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="registerFirstName">
                        Firstname
                    </Label>
                    <Input id={'registerFirstName'} type={'text'} value={firstName}
                           onChange={(event) => setFirstName(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Label for="registerLastName">
                        Lastname
                    </Label>
                    <Input id={'registerLastName'} type={'text'} value={lastName}
                           onChange={(event) => setLastName(event.target.value)}/>
                </FormGroup>
                <FormGroup>
                    <Button onClick={onSubmit}>
                        Submit
                    </Button>
                </FormGroup>
            </Form>
        </div>
    )
}
