import {Button, Input, Label} from "reactstrap";
import {MunicipalityPicker, MunicipalitySelection} from "../../shared/MunicipalityPicker";
import {useState} from "react";
import {useNavigate} from "react-router-dom";
import {Routes} from "../../../Routes";
import {AdsComponentRouteState} from "../AdsComponent";

export const LandingSearchComponent = () => {

    const [query, setQuery] = useState<string>('');
    const [citySelection, setCitySelection] = useState<MunicipalitySelection>({
        municipalityId: null
    });
    const navigate = useNavigate();

    const search = () => {
        const navigationState: AdsComponentRouteState = {
            municipalityId: citySelection.municipalityId,
            query: query,
            categoryId: null
        };
        navigate(Routes.AdsUrl, {
            state: {
                routeState: navigationState
            }
        });
    }

    return (
        <div className={'w-50 ps-4 pe-4 pt-5 pb-5'} style={{
            margin: '1rem auto 1rem 1rem'
        }}>
            <h1 className={'text-white'}>Welcome to Imesum</h1>
            <div className={'p-4'} style={{
                backgroundColor: 'rgb(255 255 255 / 66%)'
            }}>
                <Input id={'landing-search'} placeholder={'What do you want to search for?'} className={'mb-3 p-2'} value={query} onChange={(event) => setQuery(event.target.value)}/>

                <MunicipalityPicker selection={citySelection} setSelection={setCitySelection} hideLabel={true} />
                <Button onClick={search} className={'w-100 mt-3 mb-3 p-2'} color={'primary'}>Search</Button>
            </div>
        </div>
    )
}
