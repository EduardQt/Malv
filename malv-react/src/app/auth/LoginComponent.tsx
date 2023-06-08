import {Button, Form, FormFeedback, FormGroup, Input, Label} from "reactstrap";
import {useState} from "react";
import {ServiceContainer} from "../../services/ServiceContainer";
import {useNavigate} from "react-router-dom";
import {Routes} from "../../Routes";

export function LoginComponent() {
    const [email, setEmail] = useState<string>('');
    const [password, setPassword] = useState<string>('');
    const [error, setError] = useState<string>('');
    const navigate = useNavigate();

    const onSubmit = (event: any) => {
        event.preventDefault();
        ServiceContainer.authService.logIn({
            email: email,
            password: password
        }).then(result => {
            ServiceContainer.userService.authToken = result;
            ServiceContainer.userService.refreshToken = result.refreshToken;
            navigate(Routes.Index);
        }).catch(error => {
            const errors = ["EMAIL_NOT_SET", "PASSWORD_NOT_SET", "INVALID_LOGIN", ""];

            for (const key of Object.keys(error.errors)) {
                if (errors.includes(key)) {
                    setError(error.errors[key][0])
                }
            }
        });
    }

    return (
        <div className="ms-auto me-auto col-lg-6 col-md-8 col-sm-12">
            <Form>
                <FormGroup>
                    <Label for="registerEmail">
                        Email
                    </Label>
                    <Input id={'registerEmail'} type={'email'} invalid={error.length > 0} value={email}
                           onChange={(event) => setEmail(event.target.value)}/>
                    {error.length > 0 && (
                        <FormFeedback>
                            {error}
                        </FormFeedback>
                    )}
                </FormGroup>
                <FormGroup>
                    <Label for="registerPassword">
                        Password
                    </Label>
                    <Input id={'registerPassword'} type={'password'} value={password}
                           onChange={(event) => setPassword(event.target.value)}/>
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
