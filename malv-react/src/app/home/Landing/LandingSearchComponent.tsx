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
        <div className={'w-50 p-4 bg-white'} style={{
            margin: '1rem auto 1rem auto'
        }}>
            <h4>Welcome to Malv</h4>
            <Label for={'landing-search'}>
                Search
            </Label>
            <Input id={'landing-search'} placeholder={'Search'} className={'mb-3 p-2'} value={query} onChange={(event) => setQuery(event.target.value)}/>

            <MunicipalityPicker selection={citySelection} setSelection={setCitySelection} hideLabel={false} />
            <Button onClick={search} className={'w-100 mt-3 mb-3 p-2'} color={'primary'}>Search</Button>
        </div>
    )
}
