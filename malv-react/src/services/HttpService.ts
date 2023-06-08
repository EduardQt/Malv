import {UserService} from "./UserService";

export class HttpService {
    constructor(
        private readonly _userToken: UserService
    ) {
    }

    post<TResult, TData>(url: string, data: TData, contentType: string = 'application/json'): Promise<TResult> {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                headers: this.getHeaders(contentType),
                body: JSON.stringify(data),
                mode: 'cors'
            }).then(result => {
                result.json().then(parsedData => {
                    if (result.status >= 400) {
                        reject(parsedData);
                    } else {
                        resolve(parsedData);
                    }
                }).catch(error => {
                    reject(error);
                });
            }).catch(error => {
                reject(error);
            });
        })
    }

    postVoid<TData>(url: string, data: TData, contentType: string = 'application/json'): Promise<void> {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                headers: this.getHeaders(contentType),
                body: JSON.stringify(data),
                mode: 'cors'
            }).then(result => {
                if (result.status >= 400) {
                    result.json().then(parsedData => {
                        reject(parsedData);
                    }).catch(error => {
                        reject(error);
                    });
                } else {
                    resolve();
                }
            }).catch(error => {
                reject(error);
            });
        })
    }

    postFormData<TResult>(url: string, data: FormData): Promise<TResult> {
        return new Promise((resolve, reject) => {
            fetch(url, {
                method: 'POST',
                headers: this.getHeaders('multipart/form-data'),
                body: data,
                mode: 'cors'
            }).then(result => {
                result.json().then(parsedData => {
                    if (result.status > 400) {
                        reject(parsedData);
                    } else {
                        resolve(parsedData);
                    }
                }).catch(error => {
                    reject(error);
                });
            }).catch(error => {
                reject(error);
            });
        })
    }

    get<TResult>(url: string): Promise<TResult> {
        return new Promise((resolve, reject) => {
            fetch(url, {
                headers: this.getHeaders(),
                method: 'GET',
                mode: 'cors'
            }).then(result => {
                result.json().then(parsedData => {
                    if (result.status > 400) {
                        reject(parsedData);
                    } else {
                        resolve(parsedData);
                    }
                }).catch(error => {
                    reject(error);
                });
            }).catch(error => {
                reject(error);
            });
        })
    }

    getHeaders(contentType: string = 'application/json'): HeadersInit {
        if (this._userToken.authToken && contentType !== 'application/json') {
            return {
                Accept: 'application/json',
                Authorization: 'Bearer ' + this._userToken.authToken.token,
            };
        }
        if (this._userToken.authToken) {
            return {
                Accept: 'application/json',
                'Content-Type': contentType,
                Authorization: 'Bearer ' + this._userToken.authToken.token,
            };
        } else {
            return {
                Accept: 'application/json',
                'Content-Type': 'application/json',
            };
        }
    }
}
