from src.order_processing_system import Generate_id,Register_User,validate_user_id,Display_Products
from pytest import approx
import pytest

def test_validate_user_id():
    # assert valid instance
    assert validate_user_id("aliya01",["aliya01"]) == True

    # assert invalid instance
    with pytest.raises(ValueError) as e:
        assert validate_user_id("rainbow",["norainbow"])
    assert "Invalid user,create an ID via option 2"  in str(e.value)
    
# can't use assert on the remaining function some print to the screen, others return random.