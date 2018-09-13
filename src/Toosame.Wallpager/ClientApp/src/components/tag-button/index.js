import React, { Component } from 'react';
import { ButtonToolbar, Button } from 'react-bootstrap';
import history from '../../history';

export class TagButton extends Component {
    displayName = TagButton.name

    getStyleByRandom() {
        const styleList = ["primary", "success", "info", "warning", "danger"];

        let randomIndex = 0;
        do {
            randomIndex = Math.floor(Math.random() * 10);
        } while (styleList.length < randomIndex);

        return styleList[randomIndex];
    }

    render() {
        return (
            <ButtonToolbar>
                {this.props.tags.map((v, i, a) =>
                    <Button
                        key={v.tagId}
                        style={{ marginTop: 5 }}
                        bsStyle={this.getStyleByRandom()}
                        onClick={() => history.push({
                            pathname: `/tag/${v.tagId}`,
                            state: {
                                name: v.tagName,
                            },
                        })}
                    >
                        {v.tagName}
                    </Button>)}
            </ButtonToolbar>
        );
    }
}
