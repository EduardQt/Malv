import {Button, Form, FormFeedback, FormGroup, Input, Label, Spinner} from "reactstrap";
import {useSearchParams} from "react-router-dom";
import {useEffect, useState} from "react";
import {ServiceContainer} from "../../services/ServiceContainer";

export const VerifyMailComponent = () => {
    const [searchParams, setSearchParams] = useSearchParams();
    const [token, setToken] = useState<string>('');
    const [error, setError] = useState<string>('');
    const [loading, setLoading] = useState<boolean>(false);
    const [success, setSuccess] = useState<boolean>(false);

    useEffect(() => {
        const authService = ServiceContainer.authService;
        if (!searchParams.has('token')) {
            setError('Token is not specified');
            return;
        }

        const token = searchParams.get('token') as string;
        setToken(token);
        setLoading(true);
        authService.verifyMail({
            token: token
        }).then(() => {
            setError('');
            setSuccess(true);
            setLoading(false);
        }).catch(error => {
            setLoading(false);
        });
    }, []);

    const verify = () => {
        if (token.length === 0) {
            setError('Token is not specified');
            return;
        }

        const authService = ServiceContainer.authService;
        setLoading(true);
        authService.verifyMail({
            token: token
        }).then(() => {
            setError('');
            setSuccess(true);
            setLoading(false);
        }).catch(error => {
            const errors = ["INVALID_MAIL_TOKEN"];

            for (const key of Object.keys(error.errors)) {
                if (errors.includes(key)) {
                    setError(error.errors[key][0])
                }
            }
            setSuccess(false);
            setLoading(false);
        });
    }

    const renderLoader = () => {
        return (
            <div className={'d-flex flex-grow-1 justify-content-center align-items-center'}>
                <Spinner size={"lg"}/>
            </div>
        )
    }

    return loading ? renderLoader() : (
        <div className="ms-auto me-auto col-lg-6 col-md-8 col-sm-12">
            <Form>
                <FormGroup>
                    <Label for="verifyEmailToken">
                        Token
                    </Label>
                    <Input id={'verifyEmailToken'} type={'text'} valid={success} invalid={error.length > 0} value={token}
                           onChange={(event) => setToken(event.target.value)}/>
                    {(error.length > 0 || success) && (
                        <FormFeedback valid={success}>
                            {error.length > 0 ? error : 'Your email is now verified and you can log in.'}
                        </FormFeedback>
                    )}
                </FormGroup>
                <FormGroup>
                    <Button onClick={verify}>
                        Submit
                    </Button>
                </FormGroup>
            </Form>
        </div>
    )
}
