import {Dispatch, useEffect, useState} from "react";
import {ServiceContainer} from "../../services/ServiceContainer";
import {Dropdown, DropdownItem, DropdownMenu, DropdownToggle, Label} from "reactstrap";
import * as React from "react";
import {FontAwesomeIcon} from "@fortawesome/react-fontawesome";
import { faCaretDown, faCaretUp } from '@fortawesome/free-solid-svg-icons'
import {Municipality_Mod} from "../../network/Models/Municipality_Mod";

interface MunicipalityPickerProps {
    selection: MunicipalitySelection;
    setSelection: Dispatch<MunicipalitySelection>;
    hideLabel: boolean;
}

export interface MunicipalitySelection {
    municipalityId: number | null;
}

export const MunicipalityPicker = ({selection, setSelection, hideLabel}: MunicipalityPickerProps) => {
    const [dropdownOpen, setDropdownOpen] = useState<boolean>(false);
    const [municipalities, setMunicipalities] = useState<Municipality_Mod[]>([]);

    useEffect(() => {
        ServiceContainer.countryService.getCountry()
            .then(result => {
                setMunicipalities(result.country.municipalities);
            })
            .catch(error => {

            });
    }, []);

    const toggle = () => {
        setDropdownOpen(!dropdownOpen);
    }

    const select = (municipalityId: number) => {
        const newSelection: MunicipalitySelection = {
            municipalityId: municipalityId
        };
        setSelection(newSelection);
    }

    const renderMunicipalities = () => {
        return municipalities.map((value, index) => {
            return (
                <div key={'municipality-' + index}>
                    <DropdownItem onClick={() => select(value.id)}>
                        {value.name}
                    </DropdownItem>
                </div>
            )
        })
    }

    const renderSelection = () => {
        if (selection.municipalityId) {
            const municipality = municipalities.find(f => f.id === selection.municipalityId);

            return municipality?.name ?? 'None';
        }

        return 'None';
    }

    return (
        <Dropdown isOpen={dropdownOpen} className={'dropdown'} toggle={toggle}>
            {!hideLabel && <Label>
                City
            </Label>}
            <DropdownToggle tag={'div'} className={'flex-grow-1 form-control user-select-none p-2'} style={{
                cursor: 'pointer'
            }}>
                <div className={'d-flex'}>
                    <div className={'pe-2 flex-grow-1'}>{renderSelection()}</div>
                    <FontAwesomeIcon icon={dropdownOpen ? faCaretUp : faCaretDown} className={'align-self-center pe-1'} />
                </div>
            </DropdownToggle>
            <DropdownMenu style={{
                maxHeight: '300px',
                overflowX: 'scroll',
                width: '100%'
            }}>
                <DropdownItem key={-1} onClick={() => {
                    setSelection({
                        municipalityId: null
                    });
                }}>
                    None
                </DropdownItem>
                {renderMunicipalities()}
            </DropdownMenu>
        </Dropdown>
    );
}
